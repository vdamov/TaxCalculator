using FluentValidation.TestHelper;
using TaxCalculator.API.DAL.Validators;
using TaxCalculator.API.Models;
using Xunit;

namespace TaxCalculator.Tests.ValidatorTests
{
    public class TaxPayerValidatorTests
    {
        private readonly TaxPayerValidator validator;

        public TaxPayerValidatorTests()
        {
            validator = new TaxPayerValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("OneWord")]
        [InlineData("       ")]
        public async Task TaxPayer_WithInvalidFullName_ShouldReturnValidationError(string invalidFullNameValue)
        {
            //arrange
            var model = new TaxPayerModel { FullName = invalidFullNameValue };

            //act
            var result = await validator.TestValidateAsync(model);

            //assert
            result.ShouldHaveValidationErrorFor(person => person.FullName);
        }

        [Fact]
        public async Task TaxPayer_WithValidFullName_ShouldNotReturnValidationError()
        {
            //arrange
            var model = new TaxPayerModel { FullName = "Valid Name" };

            //act
            var result = await validator.TestValidateAsync(model);

            //assert
            result.ShouldNotHaveValidationErrorFor(person => person.FullName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData("3242A")]
        [InlineData("3242")]
        [InlineData("00324234398")]
        public async Task TaxPayer_WithInvalidSSN_ShouldReturnValidationError(string invalidSSNValue)
        {
            //arrange
            var model = new TaxPayerModel { SSN = invalidSSNValue };

            //act
            var result = await validator.TestValidateAsync(model);

            //assert
            result.ShouldHaveValidationErrorFor(person => person.SSN);
        }

        [Theory]
        [InlineData("0000000001")]
        [InlineData("12345")]
        public async Task TaxPayer_WithValidSSN_ShouldNotReturnValidationError(string validSSN)
        {
            //arrange
            var model = new TaxPayerModel { SSN = validSSN };

            //act
            var result = await validator.TestValidateAsync(model);

            //assert
            result.ShouldNotHaveValidationErrorFor(person => person.SSN);
        }

        [Theory]
        [InlineData(-123.32)]
        [InlineData(float.MaxValue)]
        public async Task TaxPayer_WithInvalidGrossIncome_ShouldReturnValidationError(float invalidGrossIncomeValue)
        {
            //arrange
            var model = new TaxPayerModel { GrossIncome = invalidGrossIncomeValue };

            //act
            var result = await validator.TestValidateAsync(model);

            //assert
            result.ShouldHaveValidationErrorFor(person => person.GrossIncome);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3.402823466E+37)]
        [InlineData(3400.50)]
        public async Task TaxPayer_WithValidGrossIncome_ShouldNotReturnValidationError(float invalidGrossIncomeValue)
        {
            //arrange
            var model = new TaxPayerModel { GrossIncome = invalidGrossIncomeValue };

            //act
            var result = await validator.TestValidateAsync(model);

            //assert
            result.ShouldNotHaveValidationErrorFor(person => person.GrossIncome);
        }

        [Theory]
        [InlineData(-123.32)]
        [InlineData(float.MaxValue)]
        public async Task TaxPayer_WithInvalidCharitySpent_ShouldReturnValidationError(float invalidCharityValue)
        {
            //arrange
            var model = new TaxPayerModel { CharitySpent = invalidCharityValue };

            //act
            var result = await validator.TestValidateAsync(model);

            //assert
            result.ShouldHaveValidationErrorFor(person => person.CharitySpent);
        }



        [Theory]
        [InlineData(0)]
        [InlineData(3.402823466E+37)]
        [InlineData(3400.50)]
        public async Task TaxPayer_WithValidCharitySpent_ShouldNotReturnValidationError(float invalidCharityValue)
        {
            //arrange
            var model = new TaxPayerModel { CharitySpent = invalidCharityValue };

            //act
            var result = await validator.TestValidateAsync(model);

            //assert
            result.ShouldNotHaveValidationErrorFor(person => person.CharitySpent);
        }
    }
}
