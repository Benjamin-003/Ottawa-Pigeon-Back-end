using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Ottawa.Pigeon.Application.Services.Users.Commands.UpdateUserPassword
{
    public class UserPasswordForUpdateDto
    {
        [FromQuery(Name = "old_password")]
        [JsonPropertyName("old_password")]
        public string OldPassword { get; set; } = string.Empty;

        [FromQuery(Name = "new_password")]
        [JsonPropertyName("new_password")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
