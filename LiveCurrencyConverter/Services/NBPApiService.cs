using System.Collections.Generic;
using LiveCurrencyConverter.DTO;
using LiveCurrencyConverter.Services.Interfaces;

namespace LiveCurrencyConverter.Services
{
    public class NBPApiService : INBPApiService
    {
        private readonly ILogService _logService;

        public NBPApiService(ILogService logService)
        {
            _logService = logService;
        }
        public List<RateDTO> getRates()
        {
            throw new System.NotImplementedException();
        }
    }
}