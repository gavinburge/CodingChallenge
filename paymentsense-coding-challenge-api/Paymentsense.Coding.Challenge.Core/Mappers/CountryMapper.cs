using System.Collections.Generic;
using System.Linq;
using CoreCountry = Paymentsense.Coding.Challenge.Core.Models.Country;
using ContractCountry = Paymentsense.Coding.Challenge.Contracts.Response.Country;

namespace Paymentsense.Coding.Challenge.Core.Mappers
{
    public static class CountryMapper
    {
        public static IEnumerable<ContractCountry> ToContractCountries(this IEnumerable<CoreCountry> countries)
        {
            return countries?.Select(c =>
                new ContractCountry
                {
                    Name = c.Name,
                    Flag = c.Flag
                })
                ?? new List<ContractCountry>();
        }
    }
}
