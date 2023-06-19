using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ottawa.Pigeon.Application.Common.Interfaces;

namespace Ottawa.Pigeon.Application.Services.Subscriptions.Queries
{
    public record GetSubscriptionsQuery : IRequest<List<SubscriptionDto>>;

    public class GetSubscriptionsQueryHandler : IRequestHandler<GetSubscriptionsQuery, List<SubscriptionDto>>
    {
        private readonly IOttawaPigeonDbContext _context;
        private readonly IMapper _mapper;

        public GetSubscriptionsQueryHandler(IOttawaPigeonDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<SubscriptionDto>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var subscriptionList = await _context.Subscriptions.ToListAsync(cancellationToken);

            var response = _mapper.Map<List<SubscriptionDto>>(subscriptionList);

            return response;
        }
    }
}
