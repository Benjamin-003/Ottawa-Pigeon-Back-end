using Microsoft.AspNetCore.Mvc;
using Ottawa.Pigeon.Application.Services.Users.Commands.CreateUser;
using Ottawa.Pigeon.Application.Services.Users.Commands.DeleteUser;
using Ottawa.Pigeon.Application.Services.Users.Commands.ResetUserPassword;
using Ottawa.Pigeon.Application.Services.Users.Commands.UpdateUserInfo;
using Ottawa.Pigeon.Application.Services.Users.Commands.UpdateUserPassword;
using Ottawa.Pigeon.Application.Services.Users.Queries;
using Ottawa.Pigeon.Controllers;
using Ottawa.Pigeon.Infrastructure.Authorization;

namespace Ottawa.Pigeon.WebAPI.Controllers
{
    /// <summary>
    /// Contrôleur qui gère les routes pour les collections Users
    /// </summary>
    [Authorize]
    public class UserController : ApiControllerBase
    {
        /// <summary>
        /// Route pour créer un utilisateur User
        /// 
        /// Attributs utilisés pour configurer le comportement du Controller et des méthodes
        /// Ici, les attributs spécifient le vb d'action HTTP pris en charge et l'ensemble des codes d'états connus qui peuvent être retournés
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Returns the newly created item</returns>
        /// <response code="201">Returns the newly created item </response>
        /// <response code="422">Entity unprocessable</response>
        /// <response code="409">Email already used</response>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<int>> Create(CreateUserCommand command)
        {
            await Mediator.Send(command);

            return Created("", command);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserBriefDto>> GetUserById(int id)
        {
            var user = new GetUserByIdQuery(id);

            return await Mediator.Send(user);
        }

        /// <summary>
        /// Route pour mettre à jour les informations personnelles d'un user
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="patchRequest"></param>
        /// <returns></returns>
        [HttpPatch("{user_id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> UpdateUser(int user_id, UserForUpateDto patchRequest)
        {
            var command = new UpdateUserCommand(user_id, patchRequest);

            await Mediator.Send(command);

            return NoContent();
        }
        
        /// <summary>
        /// Route pour mettre à jour le mot de passe d'un utilisateur
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="patchRequest"></param>
        /// <returns></returns>
        [HttpPatch("{user_id}/password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UpdateUserPassword(int user_id, UserPasswordForUpdateDto patchRequest)
        {
            var command = new UpdateUserPasswordCommand(user_id, patchRequest);

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{user_id}/password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> ResetUserPassword(int user_id, PasswordResetDto update)
        {
            var command = new ResetUserPasswordCommand(user_id, update);

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{user_id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteUser(int user_id)
        {
            var command = new DeleteUserCommand(user_id);

            await Mediator.Send(command);

            return NoContent();
        }
    }
}