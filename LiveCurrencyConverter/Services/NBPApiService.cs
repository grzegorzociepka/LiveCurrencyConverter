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
            addLog("Get all rates request has been sent");
            var response = await request.Content.ReadAsStringAsync();
            if (request.IsSuccessStatusCode)
            {
                var deserializedResponse =
                    JsonConvert.DeserializeObject<List<AllRatesResponseDTO>>(response).FirstOrDefault();
                return deserializedResponse.Rates.ToList();
            }
            else
            {
                addLog("External - Bad request on getRates");
                return new List<RateDTO>();
            }
        }

        public async Task<decimal> Convert(string from, string to, decimal amount)
        {
            var firstRate = await getRateToCalculate(from);
            var secondRate = await getRateToCalculate(to);
            var sold = firstRate.Ask * amount;
            var bought = sold / secondRate.Bid;
            
            addLog(string.Format("Internal - Conversion from {0} to {1}, amount {2}",from,to,amount));
            
            return Math.Round(bought, 4);
        }

        private async Task<RateDTO> getRateToCalculate(string from)
        {
            var client = new HttpClient();
            client.BaseAddress = baseAddress;
            var request = await client.GetAsync(string.Format("rates/c/{0}", from));
            addLog(string.Format("External - Get rate with code {0}",from));
            
            var response = await request.Content.ReadAsStringAsync();
            var deserializedResponse = JsonConvert.DeserializeObject<SingleRateResponseDTO>(response);
            
            var rateToReturn = new RateDTO();
            rateToReturn.Bid = deserializedResponse.Rates.FirstOrDefault().Bid;
            rateToReturn.Ask = deserializedResponse.Rates.FirstOrDefault().Ask;
            rateToReturn.Code = deserializedResponse.Code;
            return rateToReturn;
        }

        private void addLog(string desc)
        {
            var log = new LogDTO();
            log.Description = desc;
            _logService.AddLog(log);
        }
    }
}