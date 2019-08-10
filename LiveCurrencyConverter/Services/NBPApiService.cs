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

        public NBPApiService(ILogService logService)
        {
            _logService = logService;
        }

        public async Task<List<RateDTO>> getRates()
        {
            HttpClient client = createHttpClient();

            string response = await getResponse("tables/c", client);
            addLog("Get all rates request has been sent");

            AllRatesResponseDTO deserializedResponse =
                JsonConvert.DeserializeObject<List<AllRatesResponseDTO>>(response).FirstOrDefault();
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
            HttpClient client = createHttpClient();

            string response = await getResponse(string.Format("rates/c/{0}", from), client);
            addLog(string.Format("External - Get rate with code {0}", from));
            SingleRateResponseDTO deserializedResponse = JsonConvert.DeserializeObject<SingleRateResponseDTO>(response);

            var rateToReturn = new RateDTO();
            rateToReturn.Bid = deserializedResponse.Rates.FirstOrDefault().Bid;
            rateToReturn.Ask = deserializedResponse.Rates.FirstOrDefault().Ask;
            rateToReturn.Code = deserializedResponse.Code;
            return rateToReturn;
        }

        private void addLog(string desc)
        {
            _logService.AddLog(desc);
        }

        private HttpClient createHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = baseAddress;
            return client;
        }

        private async Task<string> getResponse(string requestUrl, HttpClient client)
        {
            var request = await client.GetAsync(requestUrl);
            return await request.Content.ReadAsStringAsync();
        }
    }
}