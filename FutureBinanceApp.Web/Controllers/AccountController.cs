using System;
using System.Threading.Tasks;
using FutureBinanceApp.Domain.Entities;
using FutureBinanceApp.Infrastructure.Data.Contexts;
using FutureBinanceApp.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FutureBinanceApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AccountContext _db;

        public AccountController(ILogger<AccountController> logger, AccountContext context)
        {
            _logger = logger;
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User clicks on Account Management => redirect to /Account/Index");
            var accounts = await _db.Accounts.ToListAsync();
            return View("Index", accounts);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int? id)
        {
            try
            {
                AccountModel account = await _db.Accounts.FirstOrDefaultAsync(filter => filter.Id == id);
                _db.Accounts.Remove(account);
                await _db.SaveChangesAsync();
                await BotSocketServerUtils.ReloadDataAsync("localhost", 56341 , false);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError("Account remove error");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AccountModel accountModel)
        {
            try
            {
                await _db.Accounts.AddAsync(accountModel);
                await _db.SaveChangesAsync();
                await BotSocketServerUtils.ReloadDataAsync("localhost", 56341 , false);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError("Account add error");
                throw ex;
            }

        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            return View("Update", await _db.Accounts.FirstOrDefaultAsync(filter => filter.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(AccountModel model)
        {
            try
            {
                _db.Accounts.Update(model);
                await _db.SaveChangesAsync();
                await BotSocketServerUtils.ReloadDataAsync("localhost", 56341 , false);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError("Account update error");
                throw ex;
            }
        }
    }
}
