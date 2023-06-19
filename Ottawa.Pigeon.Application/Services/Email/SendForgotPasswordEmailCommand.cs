using MediatR;
using Microsoft.EntityFrameworkCore;
using Ottawa.Pigeon.Application.Common.Interfaces;
using Ottawa.Pigeon.Application.Exceptions;
using Ottawa.Pigeon.Application.Interfaces;
using Ottawa.Pigeon.Application.Services.Email.Models;
using System.Globalization;
using System.Resources;

namespace Ottawa.Pigeon.Application.Services.Email
{
    public record SendForgotPasswordEmailCommand(string Mail) : IRequest;
    /// <summary>
    /// Classe qui envoie un mail de reinitialisation de mot de passe
    /// </summary>
    public class SendForgotPasswordEmailCommandHandler : IRequestHandler<SendForgotPasswordEmailCommand>
    {
        private readonly IOttawaPigeonDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IForgotPasswordService _forgotPasswordService;

        public SendForgotPasswordEmailCommandHandler(IOttawaPigeonDbContext context, IEmailService emailService, IForgotPasswordService forgotPasswordService)
        {
            _context = context;
            _emailService = emailService;
            _forgotPasswordService = forgotPasswordService;
        }

        public async Task<Unit> Handle(SendForgotPasswordEmailCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Mail.Equals(command.Mail), cancellationToken);
            if (user == null)
                throw new NotFoundException("Email Not Found");

            var token = _forgotPasswordService.GenerateForgotPasswordToken(user.Id);

            var link = $"http://localhost:4200/authentication/reset-password/" + token;

            var resource = _emailService.GetForgotPasswordEmailResource(user.LanguageCode);
            
            var resetPasswordEmail = new EmailDto()
            {
                To = command.Mail,
                Subject = resource.Subject!,
                Body = $"<p>{resource.PasswordResetRequest}<br>" +
                $"{resource.NoActionTaken}</p>" +
                $"<p>{resource.PasswordResetAction}<br>" +
                $"<a href=\"{link}\"><button>{resource.ResetButton}</button></a></p>" +
                $"<p>{resource.Cancellation}</p>"
            };
            _emailService.SendEmail(resetPasswordEmail);

            return Unit.Value;
        }
    }
}
