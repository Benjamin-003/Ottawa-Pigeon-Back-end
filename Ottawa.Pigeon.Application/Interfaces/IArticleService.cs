using System.ComponentModel.DataAnnotations;

namespace Ottawa.Pigeon.Application.Interfaces
{
    public interface IArticleService
    {
        public Task<string> GetArticles([Required] string url);
         
    }
}
