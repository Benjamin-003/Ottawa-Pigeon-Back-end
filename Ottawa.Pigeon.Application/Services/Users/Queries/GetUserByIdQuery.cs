using AutoMapper;
using MediatR;
using Ottawa.Pigeon.Application.Common.Interfaces;
using Ottawa.Pigeon.Application.Exceptions;
using Ottawa.Pigeon.Application.Interfaces;

namespace Ottawa.Pigeon.Application.Services.Users.Queries
{
    public record GetUserByIdQuery(int Id) : IRequest<UserBriefDto>;

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserBriefDto>
    {
        private readonly IOttawaPigeonDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public GetUserByIdQueryHandler(IOttawaPigeonDbContext context, IMapper mapper, IUserAccessor userAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<UserBriefDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
           if (_userAccessor.AllowUserAccess(request.Id))
            {
                var user = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);
                if (user == null)
                    throw new NotFoundException(nameof(user), request.Id);

                var dateOnly = user.Birthdate.Date.ToString("yyyy-MM-dd"); ;

                var userBriefDto = new UserBriefDto()
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Surname = user.Surname,
                    Birthdate = dateOnly,
                    Address = user.Address,
                    Zipcode = user.Zipcode,
                    City = user.City,
                    Country = user.Country,
                    Mail = user.Mail,
                    Newsletter = user.Newsletter,
                    LanguageCode = user.LanguageCode,
                    CurrencyCode = user.CurrencyCode,
                };
                var responseUser = _mapper.Map<UserBriefDto>(userBriefDto);
                return responseUser;
            }
           return new UserBriefDto();
        }
    }

}
