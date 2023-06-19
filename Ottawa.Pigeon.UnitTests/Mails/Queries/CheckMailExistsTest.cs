using Ottawa.Pigeon.Application.Services.Mails.Queries;
using Ottawa.Pigeon.Application.Services.Users.Commands.CreateUser;
using Ottawa.Pigeon.UnitTests.Common;

namespace Ottawa.Pigeon.UnitTests.Mails.Queries
{
    public class CheckMailExistsTest : TestBase 
    {

        /// <summary>
        /// Methode qui teste la classe pour voir si un mail n'existe pas (et donc serait unique)
        /// </summary>
        [Fact]
        public void MailShouldNotExist()
        {
            /// ARRANGE - Les variables à utiliser
            ///
            // Mise en place d'un contexte, une BDD
            var mailContext = new CheckMailExistsQueryHandler(_context);

            // Variable mail envoyé par le front
            var mail = "test2@mail.com";

            // Requête qui contient le mail
            CheckMailExistsQuery checkMail = new(mail);

            /// ACT - Appeler le handler pour qu'il retourne false ou true
            /// 
            // La méthode nécessite un arg CancellationToken, si absent on utilise .None
            var result = mailContext.Handle(checkMail, CancellationToken.None);

            /// ASSERT - Ce qui est attendu
            /// 
            // Retourne un Task<bool> donc faire apparaitre un Result (asynchrone)
            Assert.False(result.Result); 
        }


        /// <summary>
        /// Methode qui teste la classe pour voir si un mail existe déjà
        /// </summary>
        [Fact]
        public async Task MailShouldExist()
        {
            // ARRANGE
            var user = new CreateUserCommand
            {
                Surname = "User_1",
                Firstname = "User_1_first_name",
                Birthdate = DateTime.Now,
                Address = "1 Rue des Trois Rois",
                Zipcode = "69001",
                City = "Lyon",
                Country = "France",
                Mail = "test@mail.com",
                Password = "mot_de_passe",
                Newsletter = false,
            };

            var mail = "test@mail.com";

            var userContext = new CreateUserCommandHandler(_context);

            var mailContext = new CheckMailExistsQueryHandler(_context);

            CheckMailExistsQuery checkMail = new(mail);

            // ACT
            await userContext.Handle(user, CancellationToken.None);
            var result = mailContext.Handle(checkMail, CancellationToken.None);

            // ASSERT
            Assert.True(result.Result);
        }
    }
}
