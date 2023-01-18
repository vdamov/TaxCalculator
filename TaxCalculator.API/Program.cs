using FluentValidation;
using TaxCalculator.API.DAL.Entities;
using TaxCalculator.API.DAL.Repositories;
using TaxCalculator.API.Extensions;
using TaxCalculator.API.Factories;
using TaxCalculator.API.Models;

var builder = WebApplication.CreateBuilder(args);

DependencyFactory.RegisterServices(builder.Services);

var app = builder.Build();

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