using Microsoft.EntityFrameworkCore;
using TaxCalculator.API.DAL;
using TaxCalculator.API.DAL.Entities;
using TaxCalculator.API.DAL.Repositories;
using TaxCalculator.API.Models;
using TaxCalculator.Common;
using Xunit;

namespace TaxCalculator.Tests.RepositoryTests
{
    public class CalculatorRepositoryTests : IDisposable
    {
        private readonly TaxCalculatorDbContext context;
        private readonly CalculatorRepository repository;

        public CalculatorRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<TaxCalculatorDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            context = new TaxCalculatorDbContext(dbContextOptions);
            repository = new CalculatorRepository(context);
        }

        //grossIncome , charitySpent, expectedIncomeTax, expectedSocialTax, expectedTotalTax, expectedNetIncome
        [Theory]
        [InlineData(980, 0, 0, 0, 0, 980)]
        [InlineData(3400, 0, 240, 300, 540, 2860)]
        [InlineData(2500, 150, 135, 202.5, 337.5, 2162.5)]
        [InlineData(3600, 520, 224, 300, 524, 3076)]
        public async Task AddTaxPayerAsync_WithValidData_ShouldReturnCorrectTaxPayerCalculations(float grossIncome,
                                                                                                 float charitySpent,
                                                                                                 float expectedIncomeTax,
                                                                                                 float expectedSocialTax,
                                                                                                 float expectedTotalTax,
                                                                                                 float expectedNetIncome)
        {
            //arrange
            var model = new TaxPayerModel
            {
                FullName = "Valid Name",
                SSN = "00001",
                CharitySpent = charitySpent,
                GrossIncome = grossIncome
            };
            var expectedTaxPayer = new TaxPayer()
            {
                SSN = "00001",
                CharitySpent = charitySpent,
                GrossIncome = grossIncome,
                IncomeTax = expectedIncomeTax,
                NetIncome = expectedNetIncome,
                SocialTax = expectedSocialTax,
                TotalTax = expectedTotalTax
            };

            //act
            var taxPayer = await repository.AddTaxPayerAsync(model);

            //assert
            Assert.Equal(expectedTaxPayer.SSN, taxPayer.SSN);
            Assert.Equal(expectedTaxPayer.CharitySpent, taxPayer.CharitySpent);
            Assert.Equal(expectedTaxPayer.GrossIncome, taxPayer.GrossIncome);
            Assert.Equal(expectedTaxPayer.IncomeTax, taxPayer.IncomeTax);
            Assert.Equal(expectedTaxPayer.NetIncome, taxPayer.NetIncome);
            Assert.Equal(expectedTaxPayer.SocialTax, taxPayer.SocialTax);
            Assert.Equal(expectedTaxPayer.TotalTax, taxPayer.TotalTax);
        }

        [Fact]
        public async Task GetTaxPayerBySSNAsync_WithValidSSN_ShouldReturnTaxPayer()
        {
            //arrange
            var model = new TaxPayerModel
            {
                FullName = "Valid Name",
                SSN = "00001",
                CharitySpent = 2500,
                GrossIncome = 5000
            };
            await repository.AddTaxPayerAsync(model);

            //act
            var taxPayer = await repository.GetTaxPayerBySSNAsync(model.SSN);

            //assert
            Assert.NotNull(taxPayer);
            Assert.Equal(model.CharitySpent, taxPayer.CharitySpent);
            Assert.Equal(model.GrossIncome, taxPayer.GrossIncome);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
