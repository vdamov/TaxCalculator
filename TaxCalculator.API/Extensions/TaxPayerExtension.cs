using TaxCalculator.API.DAL.Entities;
using TaxCalculator.API.DTOs;

namespace TaxCalculator.API.Extensions
{
    public static class TaxPayerExtension
    {
        public static TaxPayerDto ToDto(this TaxPayer taxPayer)
        {
            return new TaxPayerDto
            {
                IncomeTax = taxPayer.IncomeTax,
                CharitySpent = taxPayer.CharitySpent,
                GrossIncome = taxPayer.GrossIncome,
                NetIncome = taxPayer.NetIncome,
                SocialTax = taxPayer.SocialTax,
                TotalTax = taxPayer.TotalTax,
            };
        }
    }
}
