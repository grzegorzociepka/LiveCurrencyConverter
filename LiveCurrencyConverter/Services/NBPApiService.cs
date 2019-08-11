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
        public static Uri baseAddress = new Uri("http://api.nbp.pl/api/exchangerates/");
        private readonly ILogService _logService;
        private HttpClient client;

        public NBPApiService(ILogService logService)
        {
            _logService = logService;
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public async Task<List<RateDTO>> getRates()
        {
            string response = await getResponse("tables/c", client);
            addLog("Get all rates request has been sent");

            AllRatesResponseDTO deserializedResponse =
                JsonConvert.DeserializeObject<List<AllRatesResponseDTO>>(response).FirstOrDefault();
            if (deserializedResponse is null)
            {
                addLog("Deserialization failed in method getRates");
                return new List<RateDTO>();
            }

            return deserializedResponse.Rates.ToList();
        }

        public async Task<decimal> Convert(string from, string to, decimal amount)
        {
            RateDTO firstRate = await getRateToCalculate(from);
            RateDTO secondRate = await getRateToCalculate(to);
            decimal sold = firstRate.Ask * amount;
            decimal bought = sold / secondRate.Bid;

            addLog(string.Format("Internal - Conversion from {0} to {1}, amount {2}", from, to, amount));

            return Math.Round(bought, 4);
        }

        private async Task<RateDTO> getRateToCalculate(string from)
        {
            string response = await getResponse(string.Format("rates/c/{0}", from), client);
            addLog(string.Format("External - Get rate with code {0}", from));
            SingleRateResponseDTO deserializedResponse = JsonConvert.DeserializeObject<SingleRateResponseDTO>(response);
            if (deserializedResponse is null)
            {
                addLog("Deserialization failed in method getRateToCalculate");
                return new RateDTO(); 
            }
            
            var rateToReturn = new RateDTO(){
                Bid = deserializedResponse.Rates.FirstOrDefault().Bid,
                Ask = deserializedResponse.Rates.FirstOrDefault().Ask,
                Code =  deserializedResponse.Code
            };
            return rateToReturn;
        }

        private void addLog(string desc)
        {
            _logService.AddLog(desc);
        }

        private async Task<string> getResponse(string requestUrl, HttpClient client)
        {
            var request = await client.GetAsync(requestUrl);
            return await request.Content.ReadAsStringAsync();
        }
    }
}