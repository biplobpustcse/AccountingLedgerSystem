using FluentValidation;

namespace AccountingLedgerSystem.Application.Accounts.Commands.CreateAccount;

public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Account name is required")
            .MaximumLength(100)
            .WithMessage("Name too long");

        RuleFor(x => x.Type)
            .NotNull()
            .NotEmpty()
            .WithMessage("Account type is required")
            .Must(type =>
                new[] { "Asset", "Liability", "Equity", "Revenue", "Expense" }.Contains(type)
            )
            .WithMessage((command, type) => $"Invalid account type: '{command.Type}'");
    }
}
