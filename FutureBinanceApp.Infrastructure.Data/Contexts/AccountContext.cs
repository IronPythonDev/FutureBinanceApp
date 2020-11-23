using FutureBinanceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FutureBinanceApp.Infrastructure.Data.Contexts
{
    public class AccountContext : DbContextEx
    {
        public DbSet<AccountModel> Accounts { get; set; }

        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AccountModel>()
                .HasIndex(x => x.Id)
                .IsUnique();
            
            base.OnModelCreating(builder);
        }
    }
}
