using System;
using Newtonsoft.Json;

namespace LiveCurrencyConverter.DTO
{
    public class SingleRateDTO
    {
        [JsonProperty("no")] 
        public string No { get; set; }

        [JsonProperty("effectiveDate")] 
        public DateTimeOffset EffectiveDate { get; set; }

        [JsonProperty("bid")] 
        public decimal Bid { get; set; }

        [JsonProperty("ask")] 
        public decimal Ask { get; set; }
    }
}