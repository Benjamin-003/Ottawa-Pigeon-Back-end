using System.ComponentModel.DataAnnotations;

namespace Ottawa.Pigeon.Infrastructure.Authorization.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Mail { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
