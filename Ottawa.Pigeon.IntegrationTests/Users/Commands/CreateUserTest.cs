using FluentAssertions;
using Ottawa.Pigeon.Application.Exceptions;
using Ottawa.Pigeon.Application.Services.Users.Commands.CreateUser;
using Ottawa.Pigeon.Domain.Entities;

namespace Ottawa.Pigeon.IntegrationTests.Users.Commands;

[Collection("Fixture collection")]
public class CreateUserTests
{

    private readonly IntegrationFixture _integrationFixture;
    public CreateUserTests(IntegrationFixture integrationFixture)
    {
        _integrationFixture = integrationFixture;
    }

    /// <summary>
    /// Test qui vérifie la validation des données lors de la création d'un User
    /// </summary>
    [Fact]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateUserCommand();

        await FluentActions.Invoking(() =>
            _integrationFixture.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Test qui vérifie la création d'un User et que les données correspondent
    /// </summary>
    [Fact]
    public async Task ShouldCreateUser()
    {
        // ARRANGE
        var command = new CreateUserCommand
        {
            Surname = "User_1",
            Firstname = "User_1_first_name",
            Birthdate = DateTime.Now,
            Address = "1 Rue des Trois Rois",
            Zipcode = "69001",
            City = "Lyon",
            Country = "France",
            Mail = "maildetest@mail.com",
            Password = "mot_de_passe",
            Newsletter = false,
            SubscriptionCode = "BA",
        };

        // ACT
        var userId = await _integrationFixture.SendAsync(command);

        var user = await _integrationFixture.FindAsync<User>(userId);

        // ASSERT
        user.Should().NotBeNull();
        user!.Surname.Should().Be(command.Surname);
        user!.Firstname.Should().Be(command.Firstname);
        user!.Birthdate.Should().Be(command.Birthdate);
        user!.Address.Should().Be(command.Address);
        user!.Zipcode.Should().Be(command.Zipcode);
        user!.City.Should().Be(command.City);
        user!.Country.Should().Be(command.Country);
        user!.Mail.Should().Be(command.Mail);
        BCrypt.Net.BCrypt.Verify(command.Password, user!.Password).Should().Be(true);
        user!.Newsletter.Should().Be(command.Newsletter);
        user!.SubscriptionCode.Should().Be(command.SubscriptionCode);

    }
}
