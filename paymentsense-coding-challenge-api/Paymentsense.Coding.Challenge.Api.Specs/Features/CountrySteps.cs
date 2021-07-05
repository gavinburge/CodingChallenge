using FluentAssertions;
using Newtonsoft.Json;
using Paymentsense.Coding.Challenge.Api.Specs.TestFramework;
using Paymentsense.Coding.Challenge.Contracts.Dtos;
using Paymentsense.Coding.Challenge.Contracts.Response;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Paymentsense.Coding.Challenge.Api.Specs.Features
{
    [Binding]
    [Scope(Feature = "Countries")]
    public class CountrySteps
    {
        private readonly HttpClient _client;
        private HttpResponseMessage _httpResponseMessage;
        private Task<HttpResponseMessage> _request1;
        private Task<HttpResponseMessage> _request2;
        private Task<HttpResponseMessage> _request3;
        private BaseApiResponseDto<PaginatedGetCountriesResponse> _getCountriesResponse;

        public CountrySteps()
        {
            _client = TestServerSetup.TestServer.CreateClient();
        }

        [Given,When(@"a request to get all countries")]
        public async Task GivenARequestToGetAllCountries()
        {
            _httpResponseMessage = await _client.GetAsync("/api/v1/countries");
        }

        [Given(@"the external data cannot be found")]
        public void GivenTheExternalDataCannotBeFound()
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
        }

        [Given(@"3 requests received to get all countries")]
        public async Task GivenRequestsReceivedToGetAllCountries()
        {
            _request1 = _client.GetAsync("/api/v1/countries");
            _request2 = _client.GetAsync("/api/v1/countries");
            _request3 = _client.GetAsync("/api/v1/countries");

            await Task.WhenAll(_request1, _request2, _request3);
        }

        [Given(@"a request to get page (.*) of countries with a page size of (.*)")]
        public async Task GivenARequestToGetPageOfCountriesWithAPageSizeOf(int pageNumber, int pageSize)
        {
            _httpResponseMessage = await _client.GetAsync($"/api/v1/countries/paginated?PageNumber={pageNumber}&PageSize={pageSize}");
        }

        [Given(@"a request to get country details with invalid characters")]
        public async Task GivenARequestToGetCountryDetailsWithInvalidCharacters()
        {
            _httpResponseMessage = await _client.GetAsync($"/api/v1/countries/detail?countryName=123@!");
        }

        [Given(@"external call returns a 500 status code")]
        public void GivenExternalCallReturnsAStatusCode()
        {
            MockServerSetup.MockServer.Reset();
            MockServerSetup.MockServer.Given(
                                        Request.Create()
                                                .WithPath("/rest/v2/all")
                                                .UsingGet())
                                        .RespondWith(
                                            Response.Create()
                                                    .WithStatusCode(500)
                                                    .WithHeader("Content-Type", "application/json")
                                                    .WithBody("Internal server error"));
        }

        [Given(@"a request to get country detail for France")]
        public async Task GivenARequestToGetCountryDetailForFrance()
        {
            _httpResponseMessage = await _client.GetAsync($"/api/v1/countries/detail?countryName=France");
        }

        [Then(@"(.*) attempts to call the service should have been made")]
        public void ThenAttemptsToCallTheServiceShouldHaveBeenMade(int p0)
        {
            MockServerSetup
                .MockServer
                .FindLogEntries(Request.Create().WithPath("/rest/v2/all").UsingGet())
                .ToList()
                .Count()
                .Should().Be(3);
        }


        [When(@"a subsequent request is made for existing data")]
        public async Task WhenASubsequentRequestIsMadeForExistingData()
        {
            MockServerSetup.Reset();

            _httpResponseMessage = await _client.GetAsync("/api/v1/countries");
        }


        [Then(@"i should get back 3 200 status")]
        public void ThenIShouldGetBackStatus()
        {
            _request1.Result.StatusCode.Should().Be(HttpStatusCode.OK);
            _request1.Result.StatusCode.Should().Be(HttpStatusCode.OK);
            _request1.Result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the external service should be called once")]
        public void ThenTheExternalServiceShouldBeCalledOnce()
        {
            MockServerSetup
                .MockServer
                .FindLogEntries(Request.Create().WithPath("/rest/v2/all").UsingGet())
                .ToList()
                .Count()
                .Should().Be(1);
        }

        [Then(@"i should get back a (.*) status")]
        public void ThenIShouldGetBackAStatus(int statusCode)
        {
            _httpResponseMessage.StatusCode.Should().Be((HttpStatusCode)statusCode);
        }

        [Then(@"i should get back (.*) countries")]
        public async Task ThenIShouldGetBackCountries(int numberOfCountries)
        {
            var responseString = await _httpResponseMessage.Content.ReadAsStringAsync();
            _getCountriesResponse = JsonConvert.DeserializeObject<BaseApiResponseDto<PaginatedGetCountriesResponse>>(responseString);

            _getCountriesResponse.Should().NotBeNull();
            _getCountriesResponse.Data.Should().NotBeNull();
            _getCountriesResponse.Data.Countries.Should().NotBeNullOrEmpty();
            _getCountriesResponse.Data.Countries.Should().HaveCount(numberOfCountries);
        }

        [Then(@"the first country name should be '(.*)'")]
        public void ThenTheFirstCountryNameShouldBe(string countryName)
        {
            _getCountriesResponse.Data.Countries.First().Name.Should().Be(countryName);            
        }

        [Then(@"the first flag should be '(.*)'")]
        public void ThenTheFirstFlagShouldBe(string flag)
        {
            _getCountriesResponse.Data.Countries.First().Flag.Should().Be(flag);
        }

        [Then(@"the last country name should be '(.*)'")]
        public void ThenTheLastCountryNameShouldBe(string countryName)
        {
            _getCountriesResponse.Data.Countries.Last().Name.Should().Be(countryName);
        }

        [Then(@"the last flag should be '(.*)'")]
        public void ThenTheLastFlagShouldBe(string flag)
        {
            _getCountriesResponse.Data.Countries.Last().Flag.Should().Be(flag);
        }

        [Then(@"the total items should be (.*)")]
        public void ThenTheTotalItemsShouldBe(int totalItems)
        {
            _getCountriesResponse.Data.TotalItems.Should().Be(totalItems);
        }

        [Then(@"i should get back detail on France")]
        public async Task ThenIShouldGetBackDetailOnFrance()
        {
            var responseString = await _httpResponseMessage.Content.ReadAsStringAsync();
            var getCountryDetail = JsonConvert.DeserializeObject<BaseApiResponseDto<GetCountryDetailResponse>>(responseString);

            getCountryDetail.Should().NotBeNull();
            getCountryDetail.Data.Should().NotBeNull();
            getCountryDetail.Data.Name.Should().Be("France");
        }
    }
}
