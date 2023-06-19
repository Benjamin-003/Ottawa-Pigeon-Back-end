using MediatR;
using Ottawa.Pigeon.Application.Common.Interfaces;
using Ottawa.Pigeon.Application.Exceptions;
using Ottawa.Pigeon.Application.Interfaces;

namespace Ottawa.Pigeon.Application.Services.Users.Commands.UpdateUserPassword
{
    public record UpdateUserPasswordCommand(int UserId, UserPasswordForUpdateDto Update) : IRequest
    {
        public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, Unit>
        {
            private readonly IOttawaPigeonDbContext _context;
            private readonly IUserAccessor _userAccessor;

            public UpdateUserPasswordCommandHandler(IOttawaPigeonDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            /// <summary>
            /// Méthode qui met à jour le mot de passe d'un utilisateur
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            /// <exception cref="NotFoundException"></exception>
            /// <exception cref="ForbiddenAccessException"></exception>
            public async Task<Unit> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
            {
                if (_userAccessor.AllowUserAccess(request.UserId))
                {
                    var user = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
                    if (user == null)
                        throw new NotFoundException("User not found.");

                    var oldPassword = request.Update.OldPassword;
                    var newPassword = request.Update.NewPassword;
                    if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
                        throw new BadRequest();

                    // Check la validité de oldPassword
                    var passwordMatch = BCrypt.Net.BCrypt.Verify(oldPassword, user.Password);
                    if (!passwordMatch)
                        throw new ForbiddenAccessException("Given current password is incorrect.");
                    
                    // Empêche d'enregistrer un mot de passe identique à l'actuel
                    if (newPassword == oldPassword)
                        throw new ForbiddenAccessException("New password must not be the same as current password.");

                    user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    await _context.SaveChangesAsync(cancellationToken);

                }
                 return Unit.Value;
            }
        }
    }
}
