using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ottawa.Pigeon.Application.Common.Interfaces;

namespace Ottawa.Pigeon.Application.Services.Languages.Queries
{
    public record GetLanguagesQuery : IRequest<List<LanguageDto>>;

    public class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, List<LanguageDto>>
    {
        private readonly IOttawaPigeonDbContext _context;
        private readonly IMapper _mapper;

        public GetLanguagesQueryHandler(IOttawaPigeonDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<LanguageDto>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            var languages = await _context.Languages.ToListAsync(cancellationToken: cancellationToken);

            var response = _mapper.Map<List<LanguageDto>>(languages);

            return response;
        }
    }
}
