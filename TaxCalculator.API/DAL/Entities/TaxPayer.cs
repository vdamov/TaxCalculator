using Microsoft.EntityFrameworkCore;
using TaxCalculator.Common.Entities;

namespace TaxCalculator.API.DAL.Entities
{
    [Index(nameof(SSN), IsUnique = true)]
    public record TaxPayer : BaseEntity<Guid>
    {
        public string SSN { get; set; }

        public float GrossIncome { get; set; }

        public float CharitySpent { get; set; }

        public float IncomeTax { get; set; }

        public float SocialTax { get; set; }

        public float TotalTax { get; set; }

        public float NetIncome { get; set; }
    }
}
