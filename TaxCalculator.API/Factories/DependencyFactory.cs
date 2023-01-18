using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.API.DAL;
using TaxCalculator.API.DAL.Repositories;
using TaxCalculator.API.DAL.Validators;
using TaxCalculator.API.Models;
using TaxCalculator.Common;

namespace TaxCalculator.API.Factories
{
    public static class DependencyFactory
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDateOnlyTimeOnlyStringConverters();
            services.AddDbContext<TaxCalculatorDbContext>(opt => opt.UseInMemoryDatabase(Constants.DbSchemaName));
            services.AddScoped<IValidator<TaxPayerModel>, TaxPayerValidator>();
            services.AddScoped<ICalculatorRepository, CalculatorRepository>();
        }
    }
}
