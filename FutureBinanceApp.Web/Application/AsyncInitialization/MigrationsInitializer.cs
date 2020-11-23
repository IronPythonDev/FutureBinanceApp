using System;
using System.Threading.Tasks;
using FutureBinanceApp.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FutureBinanceApp.Web.AsyncInitialization
{
    public class MigrationsInitializer<TDbContext> : DataInitializerBase<TDbContext>
        where TDbContext : DbContextEx
    {
        public MigrationsInitializer(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        protected override async Task InitializeAsync(TDbContext dbContext)
        {
            await dbContext.Database.MigrateAsync();
        }
    }
}
