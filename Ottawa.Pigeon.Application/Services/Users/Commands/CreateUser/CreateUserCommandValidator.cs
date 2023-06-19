using FluentValidation;

namespace Ottawa.Pigeon.Application.Services.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(v => v.Surname)
            .MaximumLength(50)
            .NotEmpty();

            RuleFor(v => v.Firstname)
            .MaximumLength(50)
            .NotEmpty();

            RuleFor(v => v.Birthdate)
            .NotEmpty();

            RuleFor(v => v.Address)
            .MaximumLength(50)
            .NotEmpty();

            RuleFor(v => v.Zipcode)
            .MaximumLength(50)
            .NotEmpty();      

            RuleFor(v => v.City)
            .MaximumLength(50)
            .NotEmpty();

            RuleFor(v => v.Country)
            .MaximumLength(50)
            .NotEmpty();

            RuleFor(v => v.Mail)
            .MaximumLength(50)
            .NotEmpty()
            .EmailAddress();

            RuleFor(v => v.Password)
            .MaximumLength(200)
            .NotEmpty();

            RuleFor(v => v.Newsletter)
            .Must(x => x == false || x == true);

            RuleFor(v => v.SubscriptionCode)
            .Length(2)
            .NotEmpty();
       
        }
    }
}
