using MediatR;
using Microsoft.EntityFrameworkCore;
using Ottawa.Pigeon.Application.Common.Interfaces;
using Ottawa.Pigeon.Application.Exceptions;
using Ottawa.Pigeon.Application.Interfaces;

namespace Ottawa.Pigeon.Application.Services.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(int UserId) : IRequest
    {
        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly IOttawaPigeonDbContext _context;
            private readonly IUserAccessor _userAccessor;

            public DeleteUserCommandHandler(IOttawaPigeonDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                if (_userAccessor.AllowUserAccess(request.UserId))
                {
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(request.UserId), cancellationToken);
                    if (user == null)
                        throw new NotFoundException("User not found.");
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
        }
    }
}
