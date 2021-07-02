using FluentAssertions;
using Moq;
using Paymentsense.Coding.Challenge.Core.Handlers;
using Paymentsense.Coding.Challenge.Core.Interfaces;
using Paymentsense.Coding.Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Core.Handlers
{
    public class PaginatedGetCountriesHandlerTests
    {
        private PaginatedGetCountriesHandler _paginatedGetCountriesHandler;
        private Mock<ICountryHttpClientService> _mockCountryHttpClientService;
        private Mock<ICachingService> _mockCache;

        public PaginatedGetCountriesHandlerTests()
        {
            _mockCountryHttpClientService = new Mock<ICountryHttpClientService>();
            _mockCache = new Mock<ICachingService>();

            _paginatedGetCountriesHandler = new PaginatedGetCountriesHandler(
                _mockCountryHttpClientService.Object,
                _mockCache.Object);
        }

        [Fact]
        public async Task GivenCountryServiceReturnsCountries_WhenCalled_ThenGetCountryResponseReturned()
        {
            _mockCache
                .Setup(s => s.GetOrAddAsync(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<Country>>>>()))
                .ReturnsAsync(new List<Country>
                {
                    new Country { Name = "UK" },
                    new Country { Name = "US" },
                    new Country { Name = "France" },
                    new Country { Name = "Germany" },
                    new Country { Name = "Spain" },
                    new Country { Name = "Italy" }
                });

            var response = await _paginatedGetCountriesHandler.Handle(
                new Contracts.Queries.PaginatedGetCountriesQuery
                {
                    PageNumber = 2,
                    PageSize = 2
                });

            response.Should().NotBeNull();
            response.Countries.Should().NotBeNullOrEmpty();
            response.Countries.Should().HaveCount(2);
            response.Countries.First().Name.Should().Be("Italy");
            response.Countries.Last().Name.Should().Be("Spain");
        }

        [Fact]
        public async Task GivenCountryServiceThrowsException_WhenCalled_ThenExceptionShouldBUbbleUp()
        {
            _mockCache
                .Setup(s => s.GetOrAddAsync(It.IsAny<string>(), It.IsAny<Func<Task<IEnumerable<Country>>>>()))
                .ThrowsAsync(new Exception("Error!"));

            await _paginatedGetCountriesHandler
                        .Invoking(g => g.Handle(new Contracts.Queries.PaginatedGetCountriesQuery
                        {
                            PageNumber = 2,
                            PageSize = 2
                        }))
                        .Should()
                        .ThrowAsync<Exception>()
                        .WithMessage("Error!");
        }
    }
}
