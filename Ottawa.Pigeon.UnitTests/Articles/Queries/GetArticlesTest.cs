using Ottawa.Pigeon.Application.Services.Articles.Queries;
using Ottawa.Pigeon.Infrastructure.ExternalAPI.Articles;
using Ottawa.Pigeon.UnitTests.Common;

namespace Ottawa.Pigeon.UnitTests.Articles.Queries
{
    public class GetArticlesTest : TestBase
    {
        /// <summary>
        /// Methode qui teste la classe GetArticlesQuery
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ResponseShouldNotBeEmpty()
        {
            // ARRANGE
            var articleService = new ArticleService();
            var context = new GetArticlesQueryHandler(articleService);
            string url = "https://www.ft.com/rss/home";
            GetArticlesQuery request = new(url);

            //ACT
            var result = await context.Handle(request, CancellationToken.None);

            // ASSERT
            Assert.NotEmpty(result);

        }
    }
}
