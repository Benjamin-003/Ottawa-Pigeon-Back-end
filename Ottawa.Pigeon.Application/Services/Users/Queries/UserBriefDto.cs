using Microsoft.AspNetCore.Mvc;
using Ottawa.Pigeon.Application.Mappings;
using Ottawa.Pigeon.Domain.Entities;
using System.Text.Json.Serialization;


namespace Ottawa.Pigeon.Application.Services.Users.Queries
{
    public class UserBriefDto : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        [FromQuery(Name = "birth_date")]
        [JsonPropertyName("birth_date")]
        public string Birthdate { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        [FromQuery(Name = "zip_code")]
        [JsonPropertyName("zip_code")]
        public string Zipcode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Mail { get;set; } = string.Empty;
        public bool Newsletter { get; set; }
        [FromQuery(Name = "language_code")]
        [JsonPropertyName("language_code")]
        public string LanguageCode { get; set; } = string.Empty;
        [FromQuery(Name = "currency_code")]
        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; } = string.Empty;
    }
}
