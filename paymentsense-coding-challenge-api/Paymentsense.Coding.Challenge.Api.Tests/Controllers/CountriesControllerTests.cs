using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Contracts.Queries;
using Paymentsense.Coding.Challenge.Contracts.Response;
using Paymentsense.Coding.Challenge.Core.Interfaces;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class CountriesControllerTests
    {
        private Mock<IRequestHandler<GetCountriesQuery, GetCountriesResponse>> _mockGetCountriesHandler;
        private Mock<IRequestHandler<PaginatedGetCountriesQuery, PaginatedGetCountriesResponse>> _mockPaginatedGetCountriesHandler;
        private CountriesController _countriesController;

        public CountriesControllerTests()
        {
            _mockGetCountriesHandler = new Mock<IRequestHandler<GetCountriesQuery, GetCountriesResponse>>();
            _mockGetCountriesHandler.Setup(c => c.Handle(It.IsAny<GetCountriesQuery>())).ReturnsAsync(new GetCountriesResponse());

            _mockPaginatedGetCountriesHandler = new Mock<IRequestHandler<PaginatedGetCountriesQuery, PaginatedGetCountriesResponse>>();

            _countriesController = new CountriesController(
                _mockGetCountriesHandler.Object,
                _mockPaginatedGetCountriesHandler.Object);
        }

        [Fact]
        public void Get_OnInvoke_ReturnsExpectedMessage()
        {
            var result = _countriesController.GetCountries().Result as OkObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
