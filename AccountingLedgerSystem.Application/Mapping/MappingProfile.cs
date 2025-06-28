using AccountingLedgerSystem.Application.Commands.Accounts;
using AccountingLedgerSystem.Application.Commands.Journal;
using AccountingLedgerSystem.Application.Commands.Users;
using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Domain.Entities;
using AutoMapper;

namespace AccountingLedgerSystem.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Account, AccountDto>().ReverseMap();
        CreateMap<CreateAccountCommand, Account>();

        CreateMap<JournalEntry, JournalEntryDto>().ReverseMap();
        CreateMap<CreateJournalEntryCommand, JournalEntry>();

        CreateMap<JournalEntryLine, JournalEntryLineDto>().ReverseMap();
        CreateMap<CreateJournalEntryCommand, JournalEntryLine>();

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ReverseMap();
        CreateMap<CreateUserCommand, User>();
    }
}
