
namespace Ottawa.Pigeon.Application.Services.Email.Models
{
    public class ForgotPasswordEmailResource
    {
        public string ?Subject { get; set; }
        public string ?PasswordResetRequest { get; set; }
        public string ?NoActionTaken { get; set; }
        public string ?PasswordResetAction { get; set; }
        public string ?ResetButton { get; set; }
        public string ?Cancellation { get; set; }
    }

}
