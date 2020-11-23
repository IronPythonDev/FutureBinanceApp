using FutureBinanceApp.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutureBinanceApp.Web.Controllers
{
    public class LogController : Controller
    {
        private LogContext Logs { get; set; }
        public LogController(LogContext context)
        {
            Logs = context;
        }
        public IActionResult Index()
        {
            return View(Logs.Logs.ToList());
        }
    }
}
