using System.Collections.Generic;

namespace Paymentsense.Coding.Challenge.Contracts.Response
{
    public class GetCountriesResponse : IResponse
    {
        public IEnumerable<CountryDto> Countries { get; set; }
    }
}
