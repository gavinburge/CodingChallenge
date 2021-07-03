using Newtonsoft.Json;
using Paymentsense.Coding.Challenge.Core.Interfaces;
using Paymentsense.Coding.Challenge.Core.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Core.Services
{
    public class CountryHttpClientService : ICountryHttpClientService
    {
        private readonly HttpClient _httpClient;

        public CountryHttpClientService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            var response = await _httpClient.GetAsync("/rest/v2/all?fields=name;flag").ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IEnumerable<Country>>(responseString);
        }
    }
}
