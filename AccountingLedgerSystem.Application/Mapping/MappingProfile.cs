using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Domain.Entities;
using AutoMapper;

namespace AccountingLedgerSystem.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Account, AccountDto>().ReverseMap();
        CreateMap<JournalEntry, JournalEntryDto>().ReverseMap();
        CreateMap<JournalEntryLine, JournalEntryLineDto>().ReverseMap();
    }
}
