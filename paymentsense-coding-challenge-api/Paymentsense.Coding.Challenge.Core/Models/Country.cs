using Newtonsoft.Json;

namespace Paymentsense.Coding.Challenge.Core.Models
{
    public class Country
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }
    }
}
