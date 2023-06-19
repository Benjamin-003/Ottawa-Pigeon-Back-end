using Microsoft.AspNetCore.Mvc;
using Ottawa.Pigeon.Application.Mappings;
using Ottawa.Pigeon.Domain.Entities;
using System.Text.Json.Serialization;

namespace Ottawa.Pigeon.Application.Services.Users.Commands.UpdateUserInfo
{
    public class UserForUpateDto : IMapFrom<User>
    {
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        [FromQuery(Name = "birth_date")]
        [JsonPropertyName("birth_date")]
        public DateTime? Birthdate { get; set; }
        public string? Address { get; set; }
        [FromQuery(Name = "zip_code")]
        [JsonPropertyName("zip_code")]
        public string? Zipcode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Mail { get; set; }
        public bool? Newsletter { get; set; }
        [FromQuery(Name = "language_code")]
        [JsonPropertyName("language_code")]
        public string? LanguageCode { get; set; }
        [FromQuery(Name = "currency_code")]
        [JsonPropertyName("currency_code")]
        public string? CurrencyCode { get; set; }
    }
}
