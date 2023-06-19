using Ottawa.Pigeon.Application.Mappings;
using Ottawa.Pigeon.Domain.Entities;

namespace Ottawa.Pigeon.Application.Services.Languages.Queries
{
    public class LanguageDto : IMapFrom<Language>
    {
        public string Code { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }
}
