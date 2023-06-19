using Ottawa.Pigeon.Application.Common.Interfaces;
using Ottawa.Pigeon.Application.Exceptions;
using Ottawa.Pigeon.Infrastructure.Authorization.Models;

namespace Ottawa.Pigeon.Infrastructure.Authorization
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest request);
    }

    /// <summary>
    /// Classe qui interagit avec la base de données et contient la logique login d'un User
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IOttawaPigeonDbContext _context;
        private readonly IJwtUtils _jwtUtils;

        public UserService(
            IOttawaPigeonDbContext context,
            IJwtUtils jwtUtils)
        {
            _context = context;
            _jwtUtils = jwtUtils;
        }

        /// <summary>
        /// Méthode qui vérifie la correspondance mail/password 
        /// </summary>
        /// <param name="request">Objet de la requete POST qui contient un mail et un password</param>
        /// <returns>Un objet response qui contient un token et l'id du user</returns>
        /// <exception cref="UnauthorizedException"></exception>
        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            var user = _context?.Users.SingleOrDefault(x => x.Mail == request.Mail);

            // validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new UnauthorizedException("Username or password is incorrect");

            var tokenValidityDuration = 60;
            // authentication successful
            AuthenticateResponse response = new()
            {
                Token = _jwtUtils.GenerateToken(user.Id, tokenValidityDuration),
                Id = user.Id
            };
            return response;
        }
    }
}
