using FutureBinanceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FutureBinanceApp.Infrastructure.Data.Contexts
{
    public class LogContext : DbContextEx
    {
        public DbSet<LogModel> Logs { get; set; }

        public LogContext(DbContextOptions<LogContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LogModel>()
                .HasIndex(x => x.Id)
                .IsUnique();

            base.OnModelCreating(builder);
        }
    }
}
