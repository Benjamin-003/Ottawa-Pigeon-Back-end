using Ottawa.Pigeon.Application.Mappings;
using Ottawa.Pigeon.Domain.Entities;

namespace Ottawa.Pigeon.Application.Services.Currencies.Queries
{
    public class CurrencyDto : IMapFrom<Currency>
    {
        public string Code { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Flag { get; set; } = string.Empty;
    }
}
