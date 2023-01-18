using FluentValidation;
using TaxCalculator.API.Models;

namespace TaxCalculator.API.DAL.Validators
{
    public class TaxPayerValidator : AbstractValidator<TaxPayerModel>
    {
        public TaxPayerValidator()
        {
            RuleFor(tp => tp.FullName)
                .Matches(@"^\w+\ \w+$")
                .NotEmpty();
            RuleFor(tp => tp.SSN).NotEmpty()
                                 .MinimumLength(5)
                                 .MaximumLength(10)
                                 .Matches("^[0-9]{5,10}$");
            RuleFor(tp => tp.GrossIncome).NotNull()
                                         .GreaterThanOrEqualTo(0)
                                         .LessThan(float.MaxValue);
            RuleFor(tp => tp.CharitySpent).NotNull()
                                     .GreaterThanOrEqualTo(0)
                                     .LessThan(float.MaxValue);
        }
    }
}
