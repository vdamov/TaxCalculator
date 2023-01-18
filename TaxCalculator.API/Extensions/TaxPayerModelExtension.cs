using TaxCalculator.API.DAL.Entities;
using TaxCalculator.API.Models;

namespace TaxCalculator.API.Extensions
{
    public static class TaxPayerModelExtension
    {
        private readonly static float noTaxationAmount = 1000;
        private readonly static float noSocialContributionsAmount = 2000;

        public static TaxPayer ToTaxPayer(this TaxPayerModel model)
        {
            float taxableAmount, incomeTax, netIncome, socialTax, totalTax;

            taxableAmount = CalculateTaxableAmount(model);
            incomeTax = CalculateIncomeTax(taxableAmount);
            socialTax = CalculateSocialTax(taxableAmount);

            totalTax = incomeTax + socialTax;
            netIncome = model.GrossIncome - totalTax;

            return new TaxPayer
            {
                SSN = model.SSN,
                CharitySpent = Convert.ToSingle(Math.Round(model.CharitySpent, 2)),
                GrossIncome = Convert.ToSingle(Math.Round(model.GrossIncome, 2)),
                IncomeTax = Convert.ToSingle(Math.Round(incomeTax, 2)),
                NetIncome = Convert.ToSingle(Math.Round(netIncome, 2)),
                SocialTax = Convert.ToSingle(Math.Round(socialTax, 2)),
                TotalTax = Convert.ToSingle(Math.Round(totalTax, 2))
            };
        }
        private static float CalculateTaxableAmount(TaxPayerModel model)
        {
            var tenPercentOfGrossIncome = model.GrossIncome * 0.1f;
            if (model.GrossIncome <= 1000)
                return 0;

            if (model.CharitySpent > tenPercentOfGrossIncome)
                return model.GrossIncome - noTaxationAmount - tenPercentOfGrossIncome;

            return model.GrossIncome - noTaxationAmount - model.CharitySpent;
        }

        private static float CalculateIncomeTax(float taxableAmount)
        {
            return taxableAmount * 0.1f;
        }

        private static float CalculateSocialTax(float taxableAmount)
        {
            if (taxableAmount > noSocialContributionsAmount)
                return noSocialContributionsAmount * 0.15f;

            return taxableAmount * 0.15f;
        }
    }
}
