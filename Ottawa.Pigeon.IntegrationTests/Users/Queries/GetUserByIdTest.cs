using FluentAssertions;
using Ottawa.Pigeon.Application.Services.Users.Commands.CreateUser;
using Ottawa.Pigeon.Application.Services.Users.Queries;

namespace Ottawa.Pigeon.IntegrationTests.Users.Queries
{
    [Collection("Fixture collection")]
    public class GetUserByIdTest
    {
        private readonly IntegrationFixture _integrationFixture;

        public GetUserByIdTest(IntegrationFixture integrationFixture)
        {
            _integrationFixture = integrationFixture;
        }
        [Fact]
        public async Task ShouldReturnUserBriefDto()
        {
            // ARRANGE
            var command = new CreateUserCommand
            {
                Surname = "MySurname",
                Firstname = "MyFirstName",
                Birthdate = DateTime.Now,
                Address = "1 Rue des Trois Rois",
                Zipcode = "69001",
                City = "Lyon",
                Country = "France",
                Mail = "myemailadress@mail.com",
                Password = "Myp@ssw0rd",
                Newsletter = false,
                SubscriptionCode = "BA",
            };


            // ACT
            var userId  = await _integrationFixture.SendAsync(command);

            _integrationFixture.AuthenticateCurrentUser(userId);

            var query = new GetUserByIdQuery(userId);

            var response = await _integrationFixture.SendAsync(query);

            // ASSERT
            response.Id.Should().Be(userId);

            response.Firstname.Should().Be(command.Firstname);

            response.Surname.Should().Be(command.Surname);

            response.Birthdate.Should().Be(command.Birthdate.Date.ToString("yyyy-MM-dd"));

            response.Address.Should().Be(command.Address);

            response.Zipcode.Should().Be(command.Zipcode);

            response.City.Should().Be(command.City);

            response.Country.Should().Be(command.Country);

            response.Mail.Should().Be(command.Mail);

            response.Newsletter.Should().Be(command.Newsletter);

            response.LanguageCode.Should().NotBeNullOrEmpty();

            response.CurrencyCode.Should().NotBeNullOrEmpty();
            
        }
    }
}
