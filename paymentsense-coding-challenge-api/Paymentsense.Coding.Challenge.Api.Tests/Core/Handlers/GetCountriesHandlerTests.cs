using FluentAssertions;
using Moq;
using Paymentsense.Coding.Challenge.Core.Handlers;
using Paymentsense.Coding.Challenge.Core.Interfaces;
using Paymentsense.Coding.Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Core.Handlers
{
    public class GetCountriesHandlerTests
    {
        private GetCountriesHandler _getCountriesHandler;
        private Mock<ICountryHttpClientService> _mockCountryHttpClientService;

        public GetCountriesHandlerTests()
        {
            _mockCountryHttpClientService = new Mock<ICountryHttpClientService>();
            _getCountriesHandler = new GetCountriesHandler(_mockCountryHttpClientService.Object);
        }

        [Fact]
        public async Task GivenCountryServiceReturnsCountries_WhenCalled_ThenGetCountryResponseReturned()
        {
            _mockCountryHttpClientService.Setup(c => c.GetCountriesAsync()).ReturnsAsync(new List<Country>
            {
                new Country { Name = "UK" },
                new Country { Name = "US" }
            });

            var response = await _getCountriesHandler.Handle(new Contracts.Queries.GetCountriesQuery());

            response.Should().NotBeNull();
            response.Countries.Should().NotBeNullOrEmpty();
            response.Countries.Should().HaveCount(2);
        }

        [Fact]
        public async Task GivenCountryServiceThrowsException_WhenCalled_ThenExceptionShouldBUbbleUp()
        {
            _mockCountryHttpClientService.Setup(c => c.GetCountriesAsync()).ThrowsAsync(new Exception("Error!"));

            await _getCountriesHandler
                        .Invoking(g => g.Handle(new Contracts.Queries.GetCountriesQuery()))
                        .Should()
                        .ThrowAsync<Exception>()
                        .WithMessage("Error!");
        }
    }
}
