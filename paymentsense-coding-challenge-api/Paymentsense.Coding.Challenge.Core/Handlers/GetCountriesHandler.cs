using Microsoft.Extensions.Caching.Memory;
using Paymentsense.Coding.Challenge.Contracts.Queries;
using Paymentsense.Coding.Challenge.Contracts.Response;
using Paymentsense.Coding.Challenge.Core.Interfaces;
using Paymentsense.Coding.Challenge.Core.Mappers;
using System;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Core.Handlers
{
    public class GetCountriesHandler : IRequestHandler<GetCountriesQuery, GetCountriesResponse>
    {
        private readonly ICountryHttpClientService _countryHttpClientService;
        private readonly ICachingService _cachingService;

        public GetCountriesHandler(
            ICountryHttpClientService countryHttpClientService,
            ICachingService cachingService)
        {
            _countryHttpClientService = countryHttpClientService;
            _cachingService = cachingService;
        }

        public async Task<GetCountriesResponse> Handle(GetCountriesQuery request)
        {
            var countries = await _cachingService.GetOrAddAsync(
                CacheKeys.AllCountries,
                () =>
                {
                    return _countryHttpClientService.GetCountriesAsync();
                }).ConfigureAwait(false);

            return new GetCountriesResponse
            {
                Countries = countries.ToContractCountries()
            };
        }
    }
}
