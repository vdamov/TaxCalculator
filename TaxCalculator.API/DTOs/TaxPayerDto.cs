namespace TaxCalculator.API.DTOs
{
    public class TaxPayerDto
    {
        public float GrossIncome { get; set; }
        public float CharitySpent { get; set; }
        public float IncomeTax { get; set; }
        public float SocialTax { get; set; }
        public float TotalTax { get; set; }
        public float NetIncome { get; set; }
    }
}
