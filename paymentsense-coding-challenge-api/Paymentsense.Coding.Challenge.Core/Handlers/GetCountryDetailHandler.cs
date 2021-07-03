using Paymentsense.Coding.Challenge.Contracts.Queries;
using Paymentsense.Coding.Challenge.Contracts.Response;
using Paymentsense.Coding.Challenge.Core.Interfaces;
using Paymentsense.Coding.Challenge.Core.Mappers;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Core.Handlers
{
    public class GetCountryDetailHandler : IRequestHandler<GetCountryDetailQuery, GetCountryDetailResponse>
    {
        private readonly ICountryHttpClientService _countryHttpClientService;
        private readonly ICachingService _cachingService;

        public GetCountryDetailHandler(
            ICountryHttpClientService countryHttpClientService,
            ICachingService cachingService)
        {
            _countryHttpClientService = countryHttpClientService;
            _cachingService = cachingService;
        }

        public async Task<GetCountryDetailResponse> Handle(GetCountryDetailQuery request)
        {
            var countryDetail = await _cachingService.GetOrAddAsync(
                $"{CacheKeys.CountryDetail}{request.CountryName}",
                () =>
                {
                    return _countryHttpClientService.GetCountryDetailAsync(request.CountryName);
                }).ConfigureAwait(false);

            return countryDetail.ToGetCountryDetailResponse();
        }
    }
}
