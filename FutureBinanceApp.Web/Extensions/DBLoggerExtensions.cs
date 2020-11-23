using FutureBinanceApp.Web.Providers;
using Microsoft.Extensions.Logging;

namespace FutureBinanceApp.Web.Extensions
{
    public static class DBLoggerExtensions
    {
        public static ILoggerFactory AddContext(this ILoggerFactory factory , string ConnectionString)
        {
            factory.AddProvider(new DBLoggerProvider(ConnectionString));
            return factory;
        }
    }
}
