using Ottawa.Pigeon.Application.Interfaces;

namespace Ottawa.Pigeon.Infrastructure.Authorization
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly IJwtUtils _jwtUtils;

        public ForgotPasswordService(IJwtUtils jwtUtils)
        {
            _jwtUtils = jwtUtils;
        }

        public string GenerateForgotPasswordToken(int id)
        {
            var tokenValidityDuration = 15;
            var token = _jwtUtils.GenerateToken(id, tokenValidityDuration);

            return token;
        }
    }
}
