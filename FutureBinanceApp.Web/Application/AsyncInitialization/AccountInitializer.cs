using System;
using System.Threading.Tasks;
using FutureBinanceApp.Domain.Entities;
using FutureBinanceApp.Infrastructure.Data.Contexts;
using FutureBinanceApp.Web.AsyncInitialization;
using Microsoft.EntityFrameworkCore;

namespace FutureBinanceApp.Web.Application.AsyncInitialization
{
    public class AccountInitializer : DataInitializerBase<AccountContext>
    {
        public AccountInitializer(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        protected override async Task InitializeAsync(AccountContext dbContext)
        {
            var count = await dbContext.Accounts.CountAsync();
            if (count > 0)
            {
                return;
            }

            await dbContext.AddAsync(new AccountModel { APIKey = "123", APISecret = "456" });
            await dbContext.SaveChangesAsync();
            //count = await dbContext.Accounts.CountAsync();
            //var account = await dbContext.Accounts.FirstOrDefaultAsync();
        }
    }
}
