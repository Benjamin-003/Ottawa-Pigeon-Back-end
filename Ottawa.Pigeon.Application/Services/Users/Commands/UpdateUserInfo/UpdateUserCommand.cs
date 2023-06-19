using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Ottawa.Pigeon.Application.Common.Interfaces;
using Ottawa.Pigeon.Application.Exceptions;
using Ottawa.Pigeon.Application.Interfaces;

namespace Ottawa.Pigeon.Application.Services.Users.Commands.UpdateUserInfo
{
    public record UpdateUserCommand(int UserId, UserForUpateDto UpdatedUser) : IRequest
    {
        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
        {
            private readonly IOttawaPigeonDbContext _context;
            private readonly IUserAccessor _userAccessor;

            public UpdateUserCommandHandler(IOttawaPigeonDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            /// <summary>
            /// Met à jour un utilisateur en fonction des champs envoyés
            /// </summary>
            /// <param name="request">un userId et le corps de requête</param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            /// <exception cref="NotFoundException"></exception>
            public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                if (_userAccessor.AllowUserAccess(request.UserId))
                {
                    var user = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
                    if (user == null)
                        throw new NotFoundException("User not found.");

                    user.Surname = !string.IsNullOrWhiteSpace(request.UpdatedUser.Surname) ? request.UpdatedUser.Surname : user.Surname;

                    user.Firstname = !string.IsNullOrWhiteSpace(request.UpdatedUser.Firstname) ? request.UpdatedUser.Firstname : user.Firstname;

                    if (request.UpdatedUser.Birthdate != null)
                        user.Birthdate = (DateTime)request.UpdatedUser.Birthdate;

                    user.Address = !string.IsNullOrWhiteSpace(request.UpdatedUser.Address) ? request.UpdatedUser.Address : user.Address;

                    user.Zipcode = !string.IsNullOrWhiteSpace(request.UpdatedUser.Zipcode) ? request.UpdatedUser.Zipcode : user.Zipcode;

                    user.City = !string.IsNullOrWhiteSpace(request.UpdatedUser.City) ? request.UpdatedUser.City : user.City;

                    user.Country = !string.IsNullOrWhiteSpace(request.UpdatedUser.Country) ? request.UpdatedUser.Country : user.Country;

                    var mail = request.UpdatedUser.Mail;
                    if (mail != null)
                    {
                        var mailExists = _context.Users.FirstOrDefault(x => x.Mail == mail);
                        if ( mailExists != null)
                            throw new UnprocessableEntityException();
                        user.Mail = mail;
                    }

                    if (request.UpdatedUser.Newsletter != null)
                        user.Newsletter = (bool)request.UpdatedUser.Newsletter;

                    user.LanguageCode = !string.IsNullOrWhiteSpace(request.UpdatedUser.LanguageCode) ? request.UpdatedUser.LanguageCode : user.LanguageCode;

                    user.CurrencyCode = !string.IsNullOrWhiteSpace(request.UpdatedUser.CurrencyCode) ? request.UpdatedUser.CurrencyCode : user.CurrencyCode;

                    await _context.SaveChangesAsync(cancellationToken);

                }
                return Unit.Value;
            }
        }
    }
}