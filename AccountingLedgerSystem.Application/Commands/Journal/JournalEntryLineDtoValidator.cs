using AccountingLedgerSystem.Application.DTOs;
using FluentValidation;

namespace AccountingLedgerSystem.Application.Commands.Journal;

public class JournalEntryLineDtoValidator : AbstractValidator<JournalEntryLineDto>
{
    public JournalEntryLineDtoValidator()
    {
        RuleFor(l => l.AccountId).GreaterThan(0).WithMessage("AccountId must be greater than 0.");
        RuleFor(l => l.Debit)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Debit must be greater than or equal to 0.");
        RuleFor(l => l.Credit)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Credit must be greater than or equal to 0.");
    }
}
