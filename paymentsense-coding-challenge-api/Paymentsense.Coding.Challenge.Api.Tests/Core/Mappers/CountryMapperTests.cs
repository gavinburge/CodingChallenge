using FluentAssertions;
using Paymentsense.Coding.Challenge.Core.Mappers;
using Paymentsense.Coding.Challenge.Core.Models;
using System.Collections.Generic;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Core.Mappers
{
    public class CountryMapperTests
    {
        [Fact]
        public void GivenListOfCountries_WhenToGetCountriesResponseCalled_GetCountriesResponseReturned()
        {
            var listOfCountries = new List<Country>
            {
                new Country { Name = "UK" },
                new Country { Name = "US" },
                new Country { Name = "France" }
            };

            var contractCountries = listOfCountries.ToContractCountries();

            contractCountries.Should().NotBeNull();
            contractCountries.Should().HaveCount(3);
        }

        [Fact]
        public void GivenListOfEmptyCountries_WhenToGetCountriesResponseCalled_GetCountriesResponseReturned()
        {
            var listOfCountries = new List<Country>();

            var contractCountries = listOfCountries.ToContractCountries();

            contractCountries.Should().NotBeNull();
            contractCountries.Should().BeEmpty();
        }

        [Fact]
        public void GivenNullListCountries_WhenToGetCountriesResponseCalled_GetCountriesResponseReturned()
        {
            List<Country> listOfCountries = null;

            var contractCountries = listOfCountries.ToContractCountries();

            contractCountries.Should().NotBeNull();
            contractCountries.Should().BeEmpty();
        }
    }
}
