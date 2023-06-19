using Ottawa.Pigeon.Application.Exceptions;
using Ottawa.Pigeon.Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ottawa.Pigeon.Infrastructure.ExternalAPI.Articles
{
    /// <summary>
    /// Classe qui interagit avec l'API externe pour le flux RSS
    /// </summary>
    public class ArticleService : IArticleService
    {
        /// <summary>
        /// Méthode qui utilise HttpClient pour consommer l'API
        /// </summary>
        /// <param name="url">URL du flux RSS</param>
        /// <returns>Une string contenant le xml du flux RSS</returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<string> GetArticles([Required] string url)
        {
            using var httpClient = new HttpClient();
            var decodedUrl = WebUtility.UrlDecode(url);

            try
            {
                var response = await httpClient.GetStringAsync(decodedUrl);
                return response;
            }
            catch
            {
                throw new NotFoundException();
            }
        }
    }
}
