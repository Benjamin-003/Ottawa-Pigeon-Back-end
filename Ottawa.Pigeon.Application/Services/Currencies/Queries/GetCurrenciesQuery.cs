using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ottawa.Pigeon.Application.Common.Interfaces;

namespace Ottawa.Pigeon.Application.Services.Currencies.Queries
{
    public record GetCurrenciesQuery : IRequest<List<CurrencyDto>>;

    public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, List<CurrencyDto>>
    {
        private readonly IOttawaPigeonDbContext _context;
        private readonly IMapper _mapper;

        public GetCurrenciesQueryHandler(IOttawaPigeonDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CurrencyDto>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
        {
            //var currencies = await _context.Currencies.ToListAsync(cancellationToken);
            //var response = _mapper.Map<List<CurrencyDto>>(currencies);

            // OU BIEN
            var response = await _context.Currencies
                            .ProjectTo<CurrencyDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

            return response;
        }
    }
}
