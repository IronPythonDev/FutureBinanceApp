using System;
using System.Diagnostics;
using FutureBinanceApp.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FutureBinanceApp.Web.Controllers
{
    public class BotController : Controller
    {
        private readonly ILogger<BotController> _logger;
        public BotController(ILogger<BotController> logger)
        {
            _logger = logger;

        }
        public IActionResult Index()
        {
            _logger.LogInformation("User clicks on Bot Control => redirect to /BotControl");
            return View(Process.GetProcessesByName("Bot"));
        }

        [HttpPost]
        public IActionResult RunBot()
        {
            try
            {
                Process process = BotsUtils.RunBotExe(@"C:\Users\Vlad\Desktop\FutureBinanceApp\Bot\bin\Debug\netcoreapp3.1\Bot.exe");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Type: {ex.GetType()} , Message: {ex.Message} , StackTrace: {ex.StackTrace}");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult KillBot()
        {
            try
            {
                BotsUtils.KillAllBots("Bot");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Type: {ex.GetType()} , Message: {ex.Message} , StackTrace: {ex.StackTrace}");
            }
            return RedirectToAction("Index");
        }

        public IActionResult KillForId(int id)
        {
            try
            {
                BotsUtils.KillBotForId(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Type: {ex.GetType()} , Message: {ex.Message} , StackTrace: {ex.StackTrace}");
            }
            return RedirectToAction("Index");
        }
    }
}
