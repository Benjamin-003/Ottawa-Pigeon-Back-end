using Microsoft.Extensions.Options;
using Ottawa.Pigeon.Application.Exceptions;
using Ottawa.Pigeon.Infrastructure;
using Ottawa.Pigeon.Infrastructure.Authorization;
using Ottawa.Pigeon.Infrastructure.Authorization.Models;
using Ottawa.Pigeon.UnitTests.Common;

namespace Ottawa.Pigeon.UnitTests.Users.Authentication
{
    public class UserServiceTest : TestBase
    {
        /// <summary>
        /// Méthode qui teste la connexion d'un utilisateur
        /// </summary>
        /// /// <returns>Une UnauthorizedException</returns>
        [Fact]
        public void AuthenticationShouldFail()
        {
            // creer un new jwtUtils qui a aussi besoin d'une dependance
            var appSettings = new AppSettings { Secret = "thisisasecret" };
            var appSettingsOptions = Options.Create(appSettings);
            var jwtUtils = new JwtUtils(appSettingsOptions);

            var context = new UserService(_context, jwtUtils);

            var request = new AuthenticateRequest
            {
                Mail = "thisismyadress@mail.com",
                Password = "thisisapassword"
            };

            // var response = context.Authenticate(request); execute et ne renvoie pas l'erreur dans response
            // ACT + ASSERT
            Assert.Throws<UnauthorizedException>(() => context.Authenticate(request));
        }
    }
}
