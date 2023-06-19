using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Ottawa.Pigeon.Application.Services.Users.Commands.ResetUserPassword
{
    public class PasswordResetDto
    {
        [FromQuery(Name = "new_password")]
        [JsonPropertyName("new_password")]
        public string? NewPassword { get; set; }
    }
}
