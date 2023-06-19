using MediatR;
using Microsoft.EntityFrameworkCore;
using Ottawa.Pigeon.Application.Common.Interfaces;

namespace Ottawa.Pigeon.Application.Services.Mails.Queries
{
    public record CheckMailExistsQuery(string Mail_id) : IRequest<bool>; // Modele qui sert a l'echange de donnees entre le front et le back

    /// <summary>
    /// Classe qui vérifie l'existence d'un mail dans la collection Users
    /// </summary>
    public class CheckMailExistsQueryHandler : IRequestHandler<CheckMailExistsQuery, bool>
    {
        private readonly IOttawaPigeonDbContext _context;

        // Constructeur
        public CheckMailExistsQueryHandler(IOttawaPigeonDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Check l'existence d'un mail
        /// </summary>
        /// <param name="request">Une adresse mail</param>
        /// <param name="cancellationToken"></param>
        /// <returns>True ou False</returns>
        public async Task<bool> Handle(CheckMailExistsQuery request, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefaultAsync(user => user.Mail == request.Mail_id, cancellationToken);
            return await user != null ? true : false;
        }

    }
}
