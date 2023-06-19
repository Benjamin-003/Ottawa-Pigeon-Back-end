using Microsoft.AspNetCore.Mvc;
using Ottawa.Pigeon.Application.Services.Subscriptions.Queries;
using Ottawa.Pigeon.Controllers;

namespace Ottawa.Pigeon.WebAPI.Controllers
{
    public class SubscriptionController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<SubscriptionDto>> GetSubscriptions()
        {
            var list = new GetSubscriptionsQuery();

            var response = await Mediator.Send(list);

            return response;
        }
    }
}
