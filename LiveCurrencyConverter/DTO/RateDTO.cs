using Newtonsoft.Json;

namespace LiveCurrencyConverter.DTO
{
    public class RateDTO
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("bid")]
        public decimal Bid { get; set; }
        [JsonProperty("set")]
        public decimal Ask { get; set; }
    }
}