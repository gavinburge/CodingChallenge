using Paymentsense.Coding.Challenge.Contracts.Response;
using System.Collections.Generic;
using System.Linq;
using CoreCountry = Paymentsense.Coding.Challenge.Core.Models.Country;
using ContractCountry = Paymentsense.Coding.Challenge.Contracts.Response.Country;

namespace Paymentsense.Coding.Challenge.Core.Mappers
{
    public static class CountryMapper
    {
        public static GetCountriesResponse ToGetCountriesResponse(this IEnumerable<CoreCountry> countries)
        {
            return new GetCountriesResponse
            {
                Countries = countries?.Select(c => new ContractCountry { Name = c.Name }) ?? new List<ContractCountry>()
            };
        }
    }
}
