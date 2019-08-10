using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LiveCurrencyConverter.DTO;
using LiveCurrencyConverter.Services.Interfaces;
using Newtonsoft.Json;

namespace LiveCurrencyConverter.Services
{
    public class NBPApiService : INBPApiService
    {
        private readonly ILogService _logService;
        public static Uri baseAddress = new Uri("http://api.nbp.pl/api/exchangerates/");

        public NBPApiService(ILogService logService)
        {
            _logService = logService;
        }

        public async Task<List<RateDTO>> getRates()
        {
            var client = new HttpClient();
            client.BaseAddress = baseAddress;
            var request = await client.GetAsync(string.Format("tables/c"));
            var response = await request.Content.ReadAsStringAsync();
            if (request.IsSuccessStatusCode)
            {
                var deserializedResponse =
                    JsonConvert.DeserializeObject<List<AllRatesResponseDTO>>(response).FirstOrDefault();
                return deserializedResponse.Rates.ToList();
            }
            else
            {
                var log = new LogDTO();
                log.Description = "Bad request";
                _logService.AddLog(log);
                return new List<RateDTO>();
            }
        }

        public async Task<decimal> Convert(string from, string to, decimal amount)
        {
            var firstRate = await getRateToCalculate(from);
            var secondRate = await getRateToCalculate(to);
            var sold = firstRate.Bid * amount;
            var bought = secondRate.Set * sold;

            return bought;
        }

        private async Task<RateDTO> getRateToCalculate(string from)
        {
            var client = new HttpClient();
            client.BaseAddress = baseAddress;
            var request = await client.GetAsync(string.Format("rates/c/{0}", from));
            var response = await request.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RateDTO>(response);
        }
    }
}