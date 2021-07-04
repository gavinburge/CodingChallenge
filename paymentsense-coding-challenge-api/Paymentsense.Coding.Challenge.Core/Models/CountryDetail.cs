using Newtonsoft.Json;
using System.Collections.Generic;

namespace Paymentsense.Coding.Challenge.Core.Models
{
    public class CountryDetail
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }

        [JsonProperty("population")]
        public int Population { get; set; }

        [JsonProperty("timeZones")]
        public IList<string> TimeZones { get; set; }

        [JsonProperty("currencies")]
        public IList<Currency> Currencies { get; set; }

        [JsonProperty("languages")]
        public IList<Language> Languages { get; set; }

        [JsonProperty("capital")]
        public string CapitalCity { get; set; }

        [JsonProperty("borders")]
        public IList<string> BorderingCountries { get; set; }
    }

    public class Currency
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }

    public class Language
    {
        [JsonProperty("iso639_1")]
        public string Iso639_1 { get; set; }

        [JsonProperty("iso639_2")]
        public string Iso639_2 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nativeName")]
        public string NativeName { get; set; }
    }
}
