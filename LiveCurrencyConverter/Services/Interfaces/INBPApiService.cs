using System.Collections.Generic;
using System.Threading.Tasks;
using LiveCurrencyConverter.DTO;

namespace LiveCurrencyConverter.Services.Interfaces
{
    public interface INBPApiService
    {
        Task<List<RateDTO>> getRates();
        Task<decimal> Convert(string from, string to, decimal amount);
    }
}