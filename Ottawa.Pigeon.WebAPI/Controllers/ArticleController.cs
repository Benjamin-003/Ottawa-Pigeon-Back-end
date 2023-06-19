using Microsoft.AspNetCore.Mvc;
using Ottawa.Pigeon.Application.Services.Articles.Queries;
using Ottawa.Pigeon.Controllers;
using Ottawa.Pigeon.Infrastructure.Authorization;

namespace Ottawa.Pigeon.WebAPI.Controllers
{
    /// <summary>
    /// Contrôleur qui gère les routes pour les flux RSS des actualités macro-économiques
    /// </summary>
    [Authorize]
    public class ArticleController : ApiControllerBase

    {
        /// <summary>
        /// Route pour récupérer le contenu d'un flux RSS
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet("{url}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> GetArticles(string url)
        {
            var articles = new GetArticlesQuery(url);

            return await Mediator.Send(articles);
        }
    }
}
