using System.Collections.Generic;
using System.Linq;
using FutureBinanceApp.Domain.Entities;
using FutureBinanceApp.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bot.Utils
{
    static class AccountData
    {
        public static List<AccountModel> Reload()
        {
            using AccountContext context = new AccountContext(new DbContextOptionsBuilder<AccountContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=futurebinanceappdb;Trusted_Connection=True;MultipleActiveResultSets=true").Options);
            return context.Accounts.ToList();
        }
    }
}
