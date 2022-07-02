using FluentValidation;
using OpenPersonalBudget.API.Contracts.Requests;

namespace OpenPersonalBudget.API.Validators
{
    public class UpdateOperationValidator : AbstractValidator<UpdateOperationRequest>
    {

        public UpdateOperationValidator()
        {
            RuleFor(o => o.Amount)
                .NotEmpty()
                .NotNull()
                .WithMessage("Amount cannot be empty")
                .Must(a => a > 0.0f)
                .WithMessage("Amount cannot be negative")
                .Must(f => f.GetType() == typeof(float))
                .WithMessage("Amount should be float");

            RuleFor(o => o.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Description cannot be empty");


            RuleFor(o => o.Category)
                .NotEmpty()
                .NotNull()
                .WithMessage("Category cannot be empty");

            RuleFor(o => o.OperationType)
                .NotEmpty()
                .NotNull()
                .WithMessage("OperationType cannot be empty");

            RuleFor(o => o.PaymentType)
                .NotEmpty()
                .NotNull()
                .WithMessage("PaymentType cannot be empty");
        }

    }
}
