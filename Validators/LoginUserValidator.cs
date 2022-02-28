using FluentValidation;
using OpenPersonalBudget.API.Contracts.Requests;

namespace OpenPersonalBudget.API.Validators
{
    public class LoginUserValidator : AbstractValidator<LoginUserRequest>
    {

        public LoginUserValidator()
        {
            RuleFor(r => r.Username).NotEmpty().NotNull().WithMessage("Username cannot be empty.");

            RuleFor(r => r.Password).NotEmpty().NotNull().WithMessage("Password cannot be empty");
        }

    }
}
