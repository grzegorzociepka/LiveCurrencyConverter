using System.Collections.Generic;
using System.Threading.Tasks;
using LiveCurrencyConverter.DTO;
using LiveCurrencyConverter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiveCurrencyConverter.Controllers
{
    /// <summary>
    /// Live currency converter
    /// </summary>
    [Route("exchange/")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly INBPApiService _nbpApiService;
        public ExchangeController(INBPApiService nbpApiService)
        {
            _nbpApiService = nbpApiService;
        }
        
        /// <summary>
        /// Get all available rates
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns recruitment info</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">If unexpected error appear</response>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _nbpApiService.getRates());
        }
        /// <summary>
        /// Convert currency
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /calculate
        ///     {
        ///        "From": "USD",
        ///        "To: "EUR",
        ///        "Amount": 20,
        ///     }
        /// </remarks>
        /// <param name="requestModel">request object</param>
        /// <response code="200">Returns calculated amount</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">If unexpected error appear</response>
        /// <returns></returns>
        [HttpPost("/calculate")]
        public async Task<ActionResult> Calculate([FromBody] RequestDTO requestModel)
        {
            return Ok(await _nbpApiService.Convert(requestModel.From, requestModel.To, requestModel.Amount));
        }
    }
}