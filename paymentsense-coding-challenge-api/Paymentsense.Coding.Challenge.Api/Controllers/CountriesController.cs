using Microsoft.AspNetCore.Mvc;
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

        public CountriesController(
            IRequestHandler<GetCountriesQuery, GetCountriesResponse> getCountriesHandler,
            IRequestHandler<PaginatedGetCountriesQuery, PaginatedGetCountriesResponse> paginatedGetCountriesHandler)
        {
            _getCountriesHandler = getCountriesHandler;
            _paginatedGetCountriesHandler = paginatedGetCountriesHandler;
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
            var response = await _getCountriesHandler.Handle(new GetCountriesQuery()).ConfigureAwait(false);

            return Ok(response);
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
            var response = await _paginatedGetCountriesHandler.Handle(paginatedGetCountriesQuery).ConfigureAwait(false);

            return Ok(response);
        }
    }
}
