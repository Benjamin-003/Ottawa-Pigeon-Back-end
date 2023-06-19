using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ottawa.Pigeon.Application.Common.Interfaces;
using Ottawa.Pigeon.Application.Exceptions;
using Ottawa.Pigeon.Domain.Entities;
using System.Text.Json.Serialization;


namespace Ottawa.Pigeon.Application.Services.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<int>
    {
        public string Surname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;

        [FromQuery(Name = "birth_date")]
        [JsonPropertyName("birth_date")]
        public DateTime Birthdate { get; set; }
        public string Address { get; set; } = string.Empty;

        [FromQuery(Name = "zip_code")]
        [JsonPropertyName("zip_code")]
        public string Zipcode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Newsletter { get; set; }

        [FromQuery(Name = "subscription_code")]
        [JsonPropertyName("subscription_code")]
        public string SubscriptionCode { get; set; } = string.Empty;

    }

    /// <summary>
    /// Classe qui crée un utilisateur User
    /// </summary>
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IOttawaPigeonDbContext _context;

        public CreateUserCommandHandler(IOttawaPigeonDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Crée un utilisateur User et crypte le mot de passe
        /// </summary>
        /// <param name="request">Les données d'un User</param>
        /// <param name="cancellationToken"></param>
        /// <returns>L'id du User</returns>
        /// <exception cref="ConflictException"></exception>
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                Surname = request.Surname,
                Firstname = request.Firstname,
                Birthdate = request.Birthdate,
                Zipcode = request.Zipcode,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                Mail = request.Mail,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Newsletter = request.Newsletter,
                LanguageCode = "FR",
                CurrencyCode = "USD",
                SubscriptionCode = request.SubscriptionCode,
        };

            // Test qui check si le mail du nouveau User existe déjà
            var user = _context.Users.FirstOrDefaultAsync(user => user.Mail == request.Mail, cancellationToken);

            if(user.Result == null)
            {
                _context.Users.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
            } else
            {
                throw new ConflictException("Email already used.");
            }

            return entity.Id;
        }
    }

}
