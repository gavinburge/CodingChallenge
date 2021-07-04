using FluentAssertions;
using Paymentsense.Coding.Challenge.Core.Validators;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Core.Validators
{
    public class GetCountryDetailQueryValidatorTests
    {
        private GetCountryDetailQueryValidator _getCountryDetailQueryValidator;

        public GetCountryDetailQueryValidatorTests()
        {
            _getCountryDetailQueryValidator = new GetCountryDetailQueryValidator();
        }

        [Theory]
        [InlineData("France", true)]
        [InlineData("Åland Islands", true)]
        [InlineData("Bolivia (Plurinational State of)", true)]
        [InlineData("Virgin Islands (U.S.)", true)]
        [InlineData("Bonaire, Sint Eustatius and Saba", true)]
        [InlineData("Curaçao", true)]
        [InlineData("Guinea-Bissau", true)]
        [InlineData("Côte d'Ivoire", true)]
        [InlineData("Germany@", false)]
        [InlineData("Germany1", false)]
        [InlineData("123", false)]
        public void GivenTheFollowingValues_ThenTheCorrectValidationResultShouldBeObtained(string countryName, bool isValid)
        {
            var result = _getCountryDetailQueryValidator.Validate(new Contracts.Queries.GetCountryDetailQuery
            {
                CountryName = countryName
            });

            result.IsValid.Should().Be(isValid);
        }
    }
}
