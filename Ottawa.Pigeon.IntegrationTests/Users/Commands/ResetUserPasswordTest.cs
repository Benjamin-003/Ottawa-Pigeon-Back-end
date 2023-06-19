using FluentAssertions;
using Ottawa.Pigeon.Application.Services.Email;
using Ottawa.Pigeon.Application.Services.Users.Commands.CreateUser;
using Ottawa.Pigeon.Application.Services.Users.Commands.ResetUserPassword;
using Ottawa.Pigeon.Domain.Entities;

namespace Ottawa.Pigeon.IntegrationTests.Users.Commands;

[Collection("Fixture collection")]
public class ResetUserPasswordTest
{
    private readonly IntegrationFixture _integrationFixture;
    public ResetUserPasswordTest(IntegrationFixture integrationFixture)
    {
        _integrationFixture = integrationFixture;
    }
    [Fact]
    public async Task PasswordShouldBeDifferent()
    {
        var command = new CreateUserCommand
        {
            Surname = "User_2",
            Firstname = "User_2_first_name",
            Birthdate = DateTime.Now,
            Address = "1 Rue des Trois Rois",
            Zipcode = "69001",
            City = "Lyon",
            Country = "France",
            Mail = "uneadressemail@mail.com",
            Password = "testmotdepasse",
            Newsletter = false,
            SubscriptionCode = "BA"
        };

        var userId = await _integrationFixture.SendAsync(command);

        var forgotPassword = new SendForgotPasswordEmailCommand(command.Mail);
        await _integrationFixture.SendAsync(forgotPassword);

        _integrationFixture.AuthenticateCurrentUser(userId);

        var passwordResetDto = new PasswordResetDto
        {
            NewPassword = "voiciunnouveaumotdepasse"
        };

        var resetPassword = new ResetUserPasswordCommand(userId, passwordResetDto);
        await _integrationFixture.SendAsync(resetPassword);

        var user = await _integrationFixture.FindAsync<User>(userId);

        BCrypt.Net.BCrypt.Verify(command.Password, user!.Password).Should().Be(false);
    }
}

