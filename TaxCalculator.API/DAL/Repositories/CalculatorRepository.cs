using Microsoft.EntityFrameworkCore;
using TaxCalculator.API.DAL.Entities;
using TaxCalculator.API.Extensions;
using TaxCalculator.API.Models;

namespace TaxCalculator.API.DAL.Repositories
{
    public class CalculatorRepository : ICalculatorRepository
    {
        private readonly TaxCalculatorDbContext dbContext;

        public CalculatorRepository(TaxCalculatorDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<TaxPayer> AddTaxPayerAsync(TaxPayerModel model)
        {
            var taxPayer = model.ToTaxPlayer();

            await dbContext.AddAsync(taxPayer);
            await dbContext.SaveChangesAsync();

            return taxPayer;
        }

        public async Task<TaxPayer?> GetTaxPayerBySSNAsync(string SSN)
        {
            var taxPayer = await dbContext.TaxPayers.FirstOrDefaultAsync(tp => tp.SSN == SSN);

            return taxPayer;
        }
    }
}
