using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests
{
    public class CountryTests
    {
        private readonly HttpClient _client;

        public CountryTests()
        {
            var builder = new WebHostBuilder().UseStartup<Startup>();
            var testServer = new TestServer(builder);
            _client = testServer.CreateClient();
        }

        [Fact]
        public async Task Health_OnInvoke_ReturnsHealthy()
        {
            var response = await _client.GetAsync("/api/v1/countries");
            var responseString = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            responseString.Should().Be("{}");
        }
    }
}
