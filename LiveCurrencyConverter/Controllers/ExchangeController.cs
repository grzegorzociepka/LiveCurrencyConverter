using System.Collections.Generic;
using System.Threading.Tasks;
using LiveCurrencyConverter.DTO;
using LiveCurrencyConverter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiveCurrencyConverter.Controllers
{
    [Route("exchange/")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly INBPApiService _nbpApiService;
        public ExchangeController(INBPApiService nbpApiService)
        {
            _nbpApiService = nbpApiService;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _nbpApiService.getRates());
        }
        [HttpPost("/calculate")]
        public async Task<ActionResult> Calculate([FromBody] RequestDTO requestModel)
        {
            return Ok(await _nbpApiService.Convert(requestModel.From, requestModel.To, requestModel.Amount));
        }
    }
}