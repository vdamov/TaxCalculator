using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.API.DAL;
using TaxCalculator.API.DAL.Entities;
using TaxCalculator.API.DAL.Repositories;
using TaxCalculator.API.DAL.Validators;
using TaxCalculator.API.Extensions;
using TaxCalculator.API.Models;
using TaxCalculator.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDateOnlyTimeOnlyStringConverters();
builder.Services.AddDbContext<TaxCalculatorDbContext>(opt => opt.UseInMemoryDatabase(Constants.DbSchemaName));
builder.Services.AddScoped<IValidator<TaxPayerModel>, TaxPayerValidator>();
builder.Services.AddScoped<ICalculatorRepository, CalculatorRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("calculator/calculate", async (IValidator<TaxPayerModel> validator,
                                           ICalculatorRepository repository,
                                           TaxPayerModel model) =>
{
    var validationResult = await validator.ValidateAsync(model);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    TaxPayer? taxPayer = await repository.GetTaxPayerBySSNAsync(model.SSN);

    if (taxPayer != null)
        return Results.Ok(taxPayer.ToDto());

    taxPayer = await repository.AddTaxPayerAsync(model);

    return Results.Ok(taxPayer.ToDto());
})
.WithName("PostCalculatorCalculate");

await app.RunAsync();