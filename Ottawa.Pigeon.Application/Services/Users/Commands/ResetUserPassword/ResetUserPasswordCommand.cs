using MediatR;
using Microsoft.EntityFrameworkCore;
using Ottawa.Pigeon.Application.Common.Interfaces;
using Ottawa.Pigeon.Application.Exceptions;
using Ottawa.Pigeon.Application.Interfaces;

namespace Ottawa.Pigeon.Application.Services.Users.Commands.ResetUserPassword
{
    public record ResetUserPasswordCommand(int User_Id, PasswordResetDto Update) : IRequest;
    /// <summary>
    /// Classe qui reinitialise le mot de passe d'un User suite à une demande de mot de passe oublié
    /// </summary>
    public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand>
    {
        private readonly IOttawaPigeonDbContext _context;
        private readonly IUserAccessor _userAccessor;

        public ResetUserPasswordCommandHandler(IOttawaPigeonDbContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }
        /// <summary>
        /// Enregistre un nouveau mot de passe d'un utilisateur et supprime l'ancien
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Unit> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (_userAccessor.AllowUserAccess(request.User_Id))
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(request.User_Id), cancellationToken);

                user!.Password = BCrypt.Net.BCrypt.HashPassword(request.Update.NewPassword);

                await _context.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
