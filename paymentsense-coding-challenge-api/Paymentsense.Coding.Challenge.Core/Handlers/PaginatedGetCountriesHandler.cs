using Paymentsense.Coding.Challenge.Contracts.Queries;
using Paymentsense.Coding.Challenge.Contracts.Response;
using Paymentsense.Coding.Challenge.Core.Interfaces;
using Paymentsense.Coding.Challenge.Core.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Core.Handlers
{
    public class PaginatedGetCountriesHandler : IRequestHandler<PaginatedGetCountriesQuery, PaginatedGetCountriesResponse>
    {
        private readonly ICountryHttpClientService _countryHttpClientService;
        private readonly ICachingService _cachingService;

        public PaginatedGetCountriesHandler(
            ICountryHttpClientService countryHttpClientService,
            ICachingService cachingService)
        {
            _countryHttpClientService = countryHttpClientService;
            _cachingService = cachingService;
        }

        public async Task<PaginatedGetCountriesResponse> Handle(PaginatedGetCountriesQuery paginatedGetCountriesQuery)
        {
            var countries = await _cachingService.GetOrAddAsync(
                CacheKeys.AllCountries,
                () =>
                {
                    return _countryHttpClientService.GetCountriesAsync();
                }).ConfigureAwait(false);

            var filteredCountries = countries.OrderBy(on => on.Name)
                                             .Skip((paginatedGetCountriesQuery.PageNumber - 1) * paginatedGetCountriesQuery.PageSize)
                                             .Take(paginatedGetCountriesQuery.PageSize);

            return new PaginatedGetCountriesResponse
            {
                PageNumber = paginatedGetCountriesQuery.PageNumber,
                PageSize = paginatedGetCountriesQuery.PageSize,
                TotalItems = countries.Count(),
                Countries = filteredCountries.ToContractCountries()
            };
        }
    }
}
