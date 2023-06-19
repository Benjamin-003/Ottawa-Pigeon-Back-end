namespace Ottawa.Pigeon.Infrastructure.Authorization.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
