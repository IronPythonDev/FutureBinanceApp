using FutureBinanceApp.Web.Utils;
using Microsoft.Extensions.Logging;

namespace FutureBinanceApp.Web.Providers
{
    public class DBLoggerProvider : ILoggerProvider
    {
        private string ConnectionString { get; set; }
        public DBLoggerProvider(string _ConnectionString)
        {
            ConnectionString = _ConnectionString;
        }
        public ILogger CreateLogger(string categoryName)
        {
           return new DBLogger(ConnectionString);
        }

        public void Dispose()
        {
        }
    }
}
