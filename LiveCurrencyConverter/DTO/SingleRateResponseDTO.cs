using Newtonsoft.Json;

namespace LiveCurrencyConverter.DTO
{
    public class SingleRateResponseDTO
    {
        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("rates")]
        public SingleRateDTO[] Rates { get; set; }
    }
}