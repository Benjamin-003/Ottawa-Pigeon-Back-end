using Microsoft.AspNetCore.Mvc;
using Ottawa.Pigeon.Application.Services.Email;
using Ottawa.Pigeon.Application.Services.Mails.Queries;
using Ottawa.Pigeon.Controllers;

namespace Ottawa.Pigeon.WebAPI.Controllers
{
    /// <summary>
    /// Contrôleur qui gère les routes pour les collections de mails
    /// </summary>
    public class MailController : ApiControllerBase
    {
        /// <summary>
        /// Une route qui teste l'existence d'un mail
        /// </summary>
        /// <param name="mail_id">Une adresse mail envoyée depuis le front</param>
        /// <returns>Un statut 200 ou 400</returns>
        [HttpHead("{mail_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CheckMailExist(string mail_id)
        {
            var query = new CheckMailExistsQuery(mail_id);

            var exists = await Mediator.Send(query);

            return exists ? Ok() : NotFound();
        }

        /// <summary>
        /// Une route qui provoque l'envoi d'un mail de reinitialisation de mot de passe
        /// </summary>
        /// <param name="mail_id">une adresse mail</param>
        /// <returns></returns>
        [HttpPost("{mail_id}/password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> SendForgotPasswordEmail(string mail_id)
        {
            var resetPasswordEmail = new SendForgotPasswordEmailCommand(mail_id);

            await Mediator.Send(resetPasswordEmail);

            return NoContent();
        }
    }
}
