using Microsoft.Extensions.Logging;
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
        private readonly ILogger<PaginatedGetCountriesHandler> _logger;

        public PaginatedGetCountriesHandler(
            ICountryHttpClientService countryHttpClientService,
            ICachingService cachingService,
            ILogger<PaginatedGetCountriesHandler> logger)
        {
            _countryHttpClientService = countryHttpClientService;
            _cachingService = cachingService;
            _logger = logger;
        }

        public async Task<PaginatedGetCountriesResponse> Handle(PaginatedGetCountriesQuery paginatedGetCountriesQuery)
        {
            var countries = await _cachingService.GetOrAddAsync(
                CacheKeys.AllCountries,
                () =>
                {
                    return _countryHttpClientService.GetCountriesAsync();
                }).ConfigureAwait(false);

            _logger.LogDebug("All countries received was {@AllCountries}", countries);

            var filteredCountries = countries.OrderBy(on => on.Name)
                                             .Skip((paginatedGetCountriesQuery.PageNumber - 1) * paginatedGetCountriesQuery.PageSize)
                                             .Take(paginatedGetCountriesQuery.PageSize);

            _logger.LogDebug(
                "Filtered countries for page {PageNumber} with page size {PageSize} was {@FilteredCountries}", 
                paginatedGetCountriesQuery.PageNumber, 
                paginatedGetCountriesQuery.PageSize, 
                countries);

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
