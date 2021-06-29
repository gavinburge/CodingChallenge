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
        private readonly IRequestHandler<GetCountriesQuery, GetCountriesResponse> _getCountriesHander;

        public CountriesController(IRequestHandler<GetCountriesQuery, GetCountriesResponse> getCountriesHander)
        {
            _getCountriesHander = getCountriesHander;
        }

        /// <summary>
        /// Gets a list of countries
        /// </summary>
        /// <returns>A list of countries with additional details</returns>
        /// <response code="404">Could not find countries</response>
        /// <response code="500">Unexpected error occurred</response>
        [Route("")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetCountriesResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCountries()
        {
            var response = await _getCountriesHander.Handle(new GetCountriesQuery());

            return Ok(response);
        }
    }
}
