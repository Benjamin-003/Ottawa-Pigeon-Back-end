using Microsoft.AspNetCore.Mvc;
using Ottawa.Pigeon.Controllers;
using Ottawa.Pigeon.Infrastructure.Authorization;
using Ottawa.Pigeon.Infrastructure.Authorization.Models;

namespace Ottawa.Pigeon.WebAPI.Controllers
{
    /// <summary>
    /// Controller qui gère les routes pour l'authentificatication d'un user
    /// </summary>
    public class TokenController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public TokenController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Route pour l'authentification d'un utilisateur
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Retourne un token d'authentifiaction</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            var response = _userService.Authenticate(request);
            return Created("", response);
        }
    }
}
