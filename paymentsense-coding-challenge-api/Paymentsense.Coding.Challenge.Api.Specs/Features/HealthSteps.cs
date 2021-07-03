using FluentAssertions;
using Paymentsense.Coding.Challenge.Api.Specs.TestFramework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Paymentsense.Coding.Challenge.Api.Specs.Features
{
    [Binding]
    [Scope(Feature = "Health")]
    public class HealthSteps
    {
        private readonly HttpClient _client;
        private HttpResponseMessage _httpResponseMessage;

        public HealthSteps()
        {
            _client = TestServerSetup.TestServer.CreateClient();
        }

        [Given(@"i call the helth check endpoint")]
        public async Task GivenICallTheHelthCheckEndpoint()
        {
            _httpResponseMessage = await _client.GetAsync("/health");
        }

        [Then(@"i should get back a (.*) status")]
        public void ThenIShouldGetBackAStatus(int status)
        {
            _httpResponseMessage.StatusCode.Should().Be((HttpStatusCode)status);
        }

        [Then(@"a healthy response is returned")]
        public async Task ThenAHealthyResponseIsReturned()
        {
            var responseString = await _httpResponseMessage.Content.ReadAsStringAsync();
            responseString.Should().Be("Healthy");
        }
    }
}
