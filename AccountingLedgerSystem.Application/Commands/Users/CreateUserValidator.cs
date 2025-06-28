using FluentValidation;

namespace AccountingLedgerSystem.Application.Commands.Users;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email is required.")
            //.EmailAddress()
            .Must(email =>
                !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".")
            )
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.");
    }
}
