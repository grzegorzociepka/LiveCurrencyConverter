namespace LiveCurrencyConverter.DTO
{
    public class RateDTO
    {
        public string Currency { get; set; }
        public string Code { get; set; }
        public decimal Bid { get; set; }
        public decimal Set { get; set; }
    }
}