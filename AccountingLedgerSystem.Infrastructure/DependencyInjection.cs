using AccountingLedgerSystem.Application.Interfaces;
using AccountingLedgerSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingLedgerSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        return services;
    }
}
