namespace TaxCalculator.API.Models
{
    public class TaxPayerModel
    {
        public string SSN { get; set; }

        public string FullName { get; set; }

        public float GrossIncome { get; set; }

        public float CharitySpent { get; set; }

        public DateOnly? DateOfBirth { get; set; }
    }
}
