using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AccountingLedgerSystem.Application.JournalEntries.Commands;

public class CreateJournalEntryValidator : AbstractValidator<CreateJournalEntryCommand>
{
    public CreateJournalEntryValidator()
    {
        RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Date cannot be null or empty.");
        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Description cannot be null or empty.");

        RuleForEach(x => x.Lines).SetValidator(new JournalEntryLineDtoValidator());

        RuleFor(x => x)
            .Must(cmd => cmd.Lines.Sum(line => line.Debit) == cmd.Lines.Sum(line => line.Credit))
            .WithMessage("Total debit must equal total credit.");
    }
}
