using AccountingLedgerSystem.Application.Interfaces;
using AccountingLedgerSystem.Infrastructure.Repositories;
using AccountingLedgerSystem.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingLedgerSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IJournalEntryRepository, JournalEntryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
