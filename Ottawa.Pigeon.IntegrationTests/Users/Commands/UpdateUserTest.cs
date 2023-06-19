using FluentAssertions;
using Ottawa.Pigeon.Application.Services.Users.Commands.CreateUser;
using Ottawa.Pigeon.Application.Services.Users.Commands.UpdateUserInfo;
using Ottawa.Pigeon.Domain.Entities;

namespace Ottawa.Pigeon.IntegrationTests.Users.Commands;

[Collection("Fixture collection")]
public class UpdatePasswordTest
{
    private readonly IntegrationFixture _integrationFixture;
    public UpdatePasswordTest(IntegrationFixture integrationFixture)
    {
        _integrationFixture = integrationFixture;
    }
    [Fact]
    public async Task InfoShouldBeDifferent()
    {
        var command = new CreateUserCommand
        {
            Surname = "DUPONT",
            Firstname = "Tintin",
            Birthdate = DateTime.Now,
            Address = "1 Rue des Trois Rois",
            Zipcode = "69001",
            City = "Lyon",
            Country = "France",
            Mail = "tintindupont@gmail.com",
            Password = "testmotdepasse",
            Newsletter = false,
            SubscriptionCode = "BA",
        };

        var userId = await _integrationFixture.SendAsync(command);

        _integrationFixture.AuthenticateCurrentUser(userId);

        var userBeforeUpdate = await _integrationFixture.FindAsync<User>(userId);

        var update = new UserForUpateDto
        {
            Surname = "DUJARDIN",
            Firstname = "Tintin2",
            Birthdate = DateTime.Parse("1994-09-17"),
            Country = "Italie",
            Newsletter = true,
            LanguageCode = "EN",
            CurrencyCode = "EUR",
        };

        var request = new UpdateUserCommand(userId, update);

        await _integrationFixture.SendAsync(request);

        var updatedUser = await _integrationFixture.FindAsync<User>(userId);

        updatedUser!.Surname.Should().NotBe(userBeforeUpdate?.Surname);
        updatedUser!.Firstname.Should().NotBe(userBeforeUpdate?.Firstname);
        updatedUser!.Birthdate.Should().NotBe(userBeforeUpdate?.Birthdate);
        updatedUser!.Country.Should().NotBe(userBeforeUpdate?.Country);
        updatedUser!.Newsletter.Should().NotBe(userBeforeUpdate!.Newsletter);
        updatedUser!.LanguageCode.Should().NotBe(userBeforeUpdate?.LanguageCode);
        updatedUser!.CurrencyCode.Should().NotBe(userBeforeUpdate?.CurrencyCode);
    }
}

