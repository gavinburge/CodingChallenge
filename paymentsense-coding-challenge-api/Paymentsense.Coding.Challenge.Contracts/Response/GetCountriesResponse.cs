using System.Collections.Generic;

namespace Paymentsense.Coding.Challenge.Contracts.Response
{
    public class GetCountriesResponse : IResponse
    {
        public IEnumerable<Country> Countries { get; set; }
    }

    public class Country
    {
        public string Name { get; set; }
    }
}
