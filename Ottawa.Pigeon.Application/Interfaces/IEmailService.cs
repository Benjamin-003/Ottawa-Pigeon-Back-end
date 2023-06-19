using Ottawa.Pigeon.Application.Services.Email.Models;

namespace Ottawa.Pigeon.Application.Interfaces
{
    public interface IEmailService
    {
        public void SendEmail(EmailDto request);

        public ForgotPasswordEmailResource GetForgotPasswordEmailResource(string languageCode);
    }
}
