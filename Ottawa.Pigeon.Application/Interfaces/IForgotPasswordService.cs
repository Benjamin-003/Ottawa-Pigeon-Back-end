namespace Ottawa.Pigeon.Application.Interfaces
{
    public interface IForgotPasswordService
    {
        public string GenerateForgotPasswordToken(int id);
    }
}
