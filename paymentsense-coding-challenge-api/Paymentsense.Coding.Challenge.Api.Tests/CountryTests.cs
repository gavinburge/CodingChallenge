using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Paymentsense.Coding.Challenge.Api.Tests.TestFramework;
using Paymentsense.Coding.Challenge.Contracts.Response;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests
{
    public class CountryTests : IDisposable
    {
        private readonly HttpClient _client;

        public CountryTests()
        {
            _client = TestServerSetup.TestServer.CreateClient();
            MockServerSetup.Start();
        }

        [Fact]
        public async Task Country_OnInvoke_ReturnsCountries()
        {
            var response = await _client.GetAsync("/api/v1/countries");
            var responseString = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            var getCountriesResponse = JsonConvert.DeserializeObject<GetCountriesResponse>(responseString);

            getCountriesResponse.Should().NotBeNull();
            getCountriesResponse.Countries.Should().NotBeNullOrEmpty();
            getCountriesResponse.Countries.Should().HaveCount(250);
        }

        [Fact]
        public async Task Country_WhenExternalErrorOccurrs_OnInvoke_ReturnsError()
        {
            MockServerSetup.MockServer.Reset();
            MockServerSetup.MockServer.Given(
                                        Request.Create()
                                                .WithPath("/rest/v2/all")
                                                .UsingGet())
                                        .RespondWith(
                                            Response.Create()
                                                    .WithStatusCode(404)
                                                    .WithHeader("Content-Type", "application/json")
                                                    .WithBody("Could not find data"));

            var response = await _client.GetAsync("/api/v1/countries");

            response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task Country_Called3Times_OnlyMakesExternalCallOnce()
        {
            var response = await _client.GetAsync("/api/v1/countries");
            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            response = await _client.GetAsync("/api/v1/countries");
            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            response = await _client.GetAsync("/api/v1/countries");
            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            MockServerSetup
                .MockServer
                .FindLogEntries(Request.Create().WithPath("/rest/v2/all").UsingGet())
                .ToList()
                .Count()
                .Should().Be(1);

        }

        [Fact]
        public async Task PaginatedCountryPage1_OnInvoke_ReturnsCountries()
        {
            var response = await _client.GetAsync("/api/v1/countries/paginated?PageNumber=1&PageSize=10");
            var responseString = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            var getCountriesResponse = JsonConvert.DeserializeObject<PaginatedGetCountriesResponse>(responseString);

            getCountriesResponse.Should().NotBeNull();
            getCountriesResponse.Countries.Should().NotBeNullOrEmpty();
            
            getCountriesResponse.Countries.Should().HaveCount(10);
            getCountriesResponse.Countries.First().Name.Should().Be("Afghanistan");
            getCountriesResponse.Countries.First().Flag.Should().Be("https://restcountries.eu/data/afg.svg");
            getCountriesResponse.Countries.Last().Name.Should().Be("Antigua and Barbuda");
            getCountriesResponse.Countries.Last().Flag.Should().Be("https://restcountries.eu/data/atg.svg");
            getCountriesResponse.PageSize.Should().Be(10);
            getCountriesResponse.PageNumber.Should().Be(1);
            getCountriesResponse.TotalItems.Should().Be(250);
        }

        [Fact]
        public async Task PaginatedCountryPage25_OnInvoke_ReturnsCountries()
        {
            var response = await _client.GetAsync("/api/v1/countries/paginated?PageNumber=25&PageSize=10");
            var responseString = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            var getCountriesResponse = JsonConvert.DeserializeObject<PaginatedGetCountriesResponse>(responseString);

            getCountriesResponse.Should().NotBeNull();
            getCountriesResponse.Countries.Should().NotBeNullOrEmpty();

            getCountriesResponse.Countries.Should().HaveCount(10);
            getCountriesResponse.Countries.First().Name.Should().Be("Vanuatu");
            getCountriesResponse.Countries.Last().Name.Should().Be("Zimbabwe");
            getCountriesResponse.PageSize.Should().Be(10);
            getCountriesResponse.PageNumber.Should().Be(25);
            getCountriesResponse.TotalItems.Should().Be(250);
        }

        void IDisposable.Dispose()
        {
            MockServerSetup.Stop();
        }
    }
}
