using AutoMapper;
using Ottawa.Pigeon.Application.Services.Currencies.Queries;
using Ottawa.Pigeon.Domain.Entities;
using Ottawa.Pigeon.UnitTests.Common;

namespace Ottawa.Pigeon.UnitTests.Currencies.Queries
{
    public class GetCurrenciesTest : TestBase
    {
        [Fact]
        public async Task CurrencyListShouldNotBeNullOrEmpty()
        {
            var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Currency, CurrencyDto>(); });
            var mapper = mapperConfig.CreateMapper();
            var context = new GetCurrenciesQueryHandler(_context, mapper);
            GetCurrenciesQuery getCurrencies = new();

            var currencyList = await context.Handle(getCurrencies, CancellationToken.None);

            Assert.NotNull(currencyList);
            Assert.NotEmpty(currencyList);
        }
    }
}
