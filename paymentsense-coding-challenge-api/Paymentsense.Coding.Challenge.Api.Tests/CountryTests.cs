using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Paymentsense.Coding.Challenge.Api.Tests.TestFramework;
using System;
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
            responseString.Should().Be("{\"countries\":[{\"name\":\"Afghanistan\"},{\"name\":\"Åland Islands\"},{\"name\":\"Albania\"},{\"name\":\"Algeria\"}]}");
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

        void IDisposable.Dispose()
        {
            MockServerSetup.Stop();
        }
    }
}
