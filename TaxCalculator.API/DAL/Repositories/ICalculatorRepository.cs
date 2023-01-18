using TaxCalculator.API.DAL.Entities;
using TaxCalculator.API.Models;

namespace TaxCalculator.API.DAL.Repositories
{
    public interface ICalculatorRepository
    {
        Task<TaxPayer> AddTaxPayerAsync(TaxPayerModel model);
        Task<TaxPayer?> GetTaxPayerBySSNAsync(string SSN);
    }
}
