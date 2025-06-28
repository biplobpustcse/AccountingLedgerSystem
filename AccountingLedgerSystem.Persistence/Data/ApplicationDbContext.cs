using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountingLedgerSystem.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();
    public DbSet<JournalEntryLine> JournalEntryLines => Set<JournalEntryLine>();
    public DbSet<User> Users => Set<User>();

    public DbSet<TrialBalanceDto> TrialBalanceResults { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //tables
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Name).IsRequired();
            entity.Property(a => a.Type).IsRequired();
        });
        modelBuilder.Entity<JournalEntryLine>(entity =>
        {
            entity.HasKey(je => je.Id);
            entity
                .HasOne(je => je.JournalEntry)
                .WithMany(je => je.Lines)
                .HasForeignKey(je => je.JournalEntryId);
            entity.HasOne(je => je.Account).WithMany().HasForeignKey(je => je.AccountId);
            entity.Property(je => je.Debit).HasDefaultValue(0);
            entity.Property(je => je.Credit).HasDefaultValue(0);
        });
        modelBuilder.Entity<JournalEntry>(entity =>
        {
            entity.HasKey(je => je.Id);
            entity.Property(je => je.Date).IsRequired();
            entity.Property(je => je.Description).IsRequired();
        });

        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        //sp
        modelBuilder.Entity<TrialBalanceDto>().HasNoKey().ToView(null); // ToView(null) prevents EF from mapping it to a table
    }
}
