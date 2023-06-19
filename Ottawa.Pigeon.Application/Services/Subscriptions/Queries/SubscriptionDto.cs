using Ottawa.Pigeon.Application.Mappings;
using Ottawa.Pigeon.Domain.Entities;

namespace Ottawa.Pigeon.Application.Services.Subscriptions.Queries
{
    public class SubscriptionDto : IMapFrom<Subscription>
    {
        public string Code { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
    }
}
