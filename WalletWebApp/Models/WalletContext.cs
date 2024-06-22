using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace WalletWebApp.Models;

public class WalletContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<WalletServiceProvider> WalletServiceProvider { get; set; }
    public DbSet<WalletServiceClientAccount> WalletServiceClientAccounts { get; set; }
    public WalletContext(DbContextOptions<WalletContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasIndex(p => p.WalletNumber)
            .IsUnique(true);
        base.OnModelCreating(builder);
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetConverter>();
        configurationBuilder.Properties<DateTimeOffset?>().HaveConversion<NullableDateTimeOffsetConverter>();
    }
}
