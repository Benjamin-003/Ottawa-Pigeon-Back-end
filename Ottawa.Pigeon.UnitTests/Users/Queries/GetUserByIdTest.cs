using AutoMapper;
using Microsoft.AspNetCore.Http;
using Ottawa.Pigeon.Application.Exceptions;
using Ottawa.Pigeon.Application.Services.Users.Queries;
using Ottawa.Pigeon.Infrastructure.Authorization;
using Ottawa.Pigeon.UnitTests.Common;

namespace Ottawa.Pigeon.UnitTests.Users.Queries
{
    public class GetUserByIdTest : TestBase
    {
        /// <summary>
        /// Méthode qui teste la classe GetUserByIdQuery
        /// </summary>
        /// <returns>Une exception NotFound</returns>
        [Fact]
        public async Task GetUserShouldReturnUnauthorizedException()
        {
            //ARRANGE
            //// Instancier une config mapper
            var mapperConfig = new MapperConfiguration(config => { });
            var mapper = mapperConfig.CreateMapper();

            var httpContextAccessor = new HttpContextAccessor();
            httpContextAccessor.HttpContext = new DefaultHttpContext();
            httpContextAccessor.HttpContext.Items["UserId"] = 1;

            var userAccessor = new UserAccessor(httpContextAccessor);

            var context = new GetUserByIdQueryHandler(_context, mapper, userAccessor);

            var id = 12;

            // SAME AS var getUser = new GetUserByIdQuery(id);
            GetUserByIdQuery getUser = new(id);

            // ACT + ASSERT
            await Assert.ThrowsAsync<UnauthorizedException>(() => context.Handle(getUser, CancellationToken.None));
        }
    }
}
