using AutoMapper;
using Ottawa.Pigeon.Application.Services.Languages.Queries;
using Ottawa.Pigeon.Domain.Entities;
using Ottawa.Pigeon.UnitTests.Common;

namespace Ottawa.Pigeon.UnitTests.Languages.Queries
{
    public class GetLanguagesTest : TestBase
    {
        [Fact]
        public async Task LanguageListShouldNotBeNullOrEmpty()
        {
            var mapperConfig = new MapperConfiguration(config => { config.CreateMap<Language, LanguageDto>(); });
            var mapper = mapperConfig.CreateMapper();

            var context = new GetLanguagesQueryHandler(_context, mapper);
            GetLanguagesQuery getLanguages = new();

            var list = await context.Handle(getLanguages, CancellationToken.None);

            Assert.NotNull(list);
            Assert.NotEmpty(list);
        }
    }
}
