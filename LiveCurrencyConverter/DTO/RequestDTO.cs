using System;

namespace LiveCurrencyConverter.DTO
{
    public class RequestDTO
    {
        public string From { get; set; }
        public string To { get; set; }
        public Decimal Amount { get; set; }
        
    }
}