using Microsoft.AspNetCore.Mvc;
using Ottawa.Pigeon.Application.Services.Currencies.Queries;
using Ottawa.Pigeon.Controllers;

namespace Ottawa.Pigeon.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/currencies")]
    public class CurrencyController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<CurrencyDto>> GetCurrencies()
        {
            var currencies = new GetCurrenciesQuery();
            var response = await Mediator.Send(currencies);
            return response;
        }
    }
}
