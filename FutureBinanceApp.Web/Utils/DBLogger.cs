using System;
using FutureBinanceApp.Domain.Entities;
using FutureBinanceApp.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FutureBinanceApp.Web.Utils
{
    public class DBLogger : ILogger
    {
        private string ConnectionString { get; set; }
        public DBLogger(string _connectionString)
        {
            ConnectionString = _connectionString;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            try
            {
                using LogContext context = new LogContext(new DbContextOptionsBuilder<LogContext>().UseSqlServer(ConnectionString).Options);
                var err = new LogModel() { LogLevel = logLevel, Message = formatter(state, exception) , Date = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString() };
                context.Add(err);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
