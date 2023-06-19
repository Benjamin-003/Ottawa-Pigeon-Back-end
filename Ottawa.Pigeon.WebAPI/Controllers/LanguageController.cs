using Microsoft.AspNetCore.Mvc;
using Ottawa.Pigeon.Application.Services.Languages.Queries;
using Ottawa.Pigeon.Controllers;

namespace Ottawa.Pigeon.WebAPI.Controllers
{
    public class LanguageController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<LanguageDto>> GetLanguages()
        {
            var languages = new GetLanguagesQuery();

            var response = await Mediator.Send(languages);

            return response;
        }
    }
}
