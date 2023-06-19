using Ottawa.Pigeon.Domain.Common;

namespace Ottawa.Pigeon.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Surname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public DateTime Birthdate { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Newsletter { get; set; }

        public string LanguageCode { get; set; } = string.Empty;
        public virtual Language Language { get; set; } = null!;

        public string CurrencyCode { get; set; } = string.Empty;
        public virtual Currency Currency { get; set; } = null!;

        public string SubscriptionCode { get; set; } = string.Empty;
        public virtual Subscription Subscription { get; set; } = null!;
    }
}
