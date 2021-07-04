using Paymentsense.Coding.Challenge.Contracts.Dtos;
using System.Collections.Generic;

namespace Paymentsense.Coding.Challenge.Contracts.Response
{
    public class GetCountryDetailResponse : IResponse
    {
        public string Name { get; set; }
        public string Flag { get; set; }
        public int Population { get; set; }
        public IList<string> TimeZones { get; set; }
        public IList<CurrencyDto> Currencies { get; set; }
        public IList<LanguageDto> Languages { get; set; }
        public string CapitalCity { get; set; }
        public IList<string> BorderingCountries { get; set; }
    }
}
