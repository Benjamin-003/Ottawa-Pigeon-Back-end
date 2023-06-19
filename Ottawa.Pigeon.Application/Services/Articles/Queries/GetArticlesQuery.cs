using MediatR;
using Ottawa.Pigeon.Application.Interfaces;

namespace Ottawa.Pigeon.Application.Services.Articles.Queries
{
    public record GetArticlesQuery(string url) : IRequest<string>;
    /// <summary>
    /// Classe qui récupère le flux RSS
    /// </summary>
    public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, string>
    {
        private readonly IArticleService _articleService;
        public GetArticlesQueryHandler(IArticleService articleService)
        {
            _articleService = articleService;
        }

        /// <summary>
        /// Handler qui appelle la méthode GetArticles
        /// </summary>
        /// <param name="request">Une URL</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            return await _articleService.GetArticles(request.url);
        }

    }
}

