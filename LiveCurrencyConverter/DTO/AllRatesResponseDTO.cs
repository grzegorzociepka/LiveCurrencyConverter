using System;
using Newtonsoft.Json;

namespace LiveCurrencyConverter.DTO
{
    public class AllRatesResponseDTO
    {
        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("no")]
        public string No { get; set; }

        [JsonProperty("tradingDate")]
        public DateTimeOffset TradingDate { get; set; }

        [JsonProperty("effectiveDate")]
        public DateTimeOffset EffectiveDate { get; set; }

        [JsonProperty("rates")]
        public RateDTO[] Rates { get; set; }
    }
}