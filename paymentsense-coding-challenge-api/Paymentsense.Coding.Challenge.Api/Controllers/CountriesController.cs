using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paymentsense.Coding.Challenge.Contracts.Dtos;
using Paymentsense.Coding.Challenge.Contracts.Queries;
using Paymentsense.Coding.Challenge.Contracts.Response;
using Paymentsense.Coding.Challenge.Core.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("api/v1/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly IRequestHandler<GetCountriesQuery, GetCountriesResponse> _getCountriesHandler;
        private readonly IRequestHandler<PaginatedGetCountriesQuery, PaginatedGetCountriesResponse> _paginatedGetCountriesHandler;
        private readonly IRequestHandler<GetCountryDetailQuery, GetCountryDetailResponse> _getCountryDetailHandler;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController(
            IRequestHandler<GetCountriesQuery, GetCountriesResponse> getCountriesHandler,
            IRequestHandler<PaginatedGetCountriesQuery, PaginatedGetCountriesResponse> paginatedGetCountriesHandler,
            IRequestHandler<GetCountryDetailQuery, GetCountryDetailResponse> getCountryDetailHandler,
            ILogger<CountriesController> logger)
        {
            _getCountriesHandler = getCountriesHandler;
            _paginatedGetCountriesHandler = paginatedGetCountriesHandler;
            _getCountryDetailHandler = getCountryDetailHandler;
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of countries
        /// </summary>
        /// <returns>A list of countries</returns>
        /// <response code="404">Could not find countries</response>
        /// <response code="500">Unexpected error occurred</response>
        [Route("")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetCountriesResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCountries()
        {
            _logger.LogInformation("Received GET country request");

            var response = await _getCountriesHandler.Handle(new GetCountriesQuery()).ConfigureAwait(false);

            _logger.LogDebug("Responding from GET country request with {@Response}", response);

            return Ok(BaseApiResponseDto<GetCountriesResponse>.SuccessResult(response));
        }

        /// <summary>
        /// Gets a paged list of countries
        /// </summary>
        /// <returns>A paged list of countries</returns>
        /// <response code="404">Could not find countries</response>
        /// <response code="500">Unexpected error occurred</response>
        [Route("paginated")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaginatedGetCountriesResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PaginatedGetCountries([FromQuery] PaginatedGetCountriesQuery paginatedGetCountriesQuery)
        {
            _logger.LogInformation("Received GET paginated country request for page number {PageNumber} and page size {PageSize}", paginatedGetCountriesQuery.PageNumber, paginatedGetCountriesQuery.PageSize);

            var response = await _paginatedGetCountriesHandler.Handle(paginatedGetCountriesQuery).ConfigureAwait(false);

            _logger.LogDebug("Responding from GET paginated country request with {@Response}", response);

            return Ok(BaseApiResponseDto<PaginatedGetCountriesResponse>.SuccessResult(response));
        }

        /// <summary>
        /// Gets details of a specific country
        /// </summary>
        /// <returns>returns details for a specidic country such as population, time zones, currencies, language, capital city and bordering countries</returns>
        /// <response code="404">Could not find specidic country</response>
        /// <response code="500">Unexpected error occurred</response>
        [Route("detail")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetCountryDetailResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCountryDetail([FromQuery] GetCountryDetailQuery getCountryDetailQuery)
        {
            var response = await _getCountryDetailHandler.Handle(getCountryDetailQuery).ConfigureAwait(false);

            return Ok(BaseApiResponseDto<GetCountryDetailResponse>.SuccessResult(response));
        }
    }
}
