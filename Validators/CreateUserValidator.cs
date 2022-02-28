using FluentValidation;
using OpenPersonalBudget.API.Contracts.Requests;

namespace OpenPersonalBudget.API.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {

        public CreateUserValidator()
        {
            RuleFor(r => r.Username)
                .NotEmpty()
                .NotNull()
                .WithMessage("Username cannot be empty");

            RuleFor(r => r.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Password cannot be empty")
                .Length(8, 64)
                .WithMessage("Password must have 8 characters");

            RuleFor(r => r.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email cannot be empty")
                .EmailAddress()
                .WithMessage("Invalid email");
        }

    }
}
