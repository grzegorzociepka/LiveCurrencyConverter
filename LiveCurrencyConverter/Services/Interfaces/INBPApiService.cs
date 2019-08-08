using System.Collections.Generic;
using LiveCurrencyConverter.DTO;

namespace LiveCurrencyConverter.Services.Interfaces
{
    public interface INBPApiService
    {
        List<RateDTO> getRates();
        
    }
}