using Paymentsense.Coding.Challenge.Contracts.Queries;
using Paymentsense.Coding.Challenge.Contracts.Response;
using Paymentsense.Coding.Challenge.Core.Interfaces;
using Paymentsense.Coding.Challenge.Core.Mappers;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Core.Handlers
{
    public class GetCountriesHandler : IRequestHandler<GetCountriesQuery, GetCountriesResponse>
    {
        private readonly ICountryHttpClientService _countryHttpClientService;

        public GetCountriesHandler(ICountryHttpClientService countryHttpClientService)
        {
            _countryHttpClientService = countryHttpClientService;
        }

        public async Task<GetCountriesResponse> Handle(GetCountriesQuery request)
        {
            var countries = await _countryHttpClientService.GetCountriesAsync();

            return countries.ToGetCountriesResponse();
        }
    }
}
