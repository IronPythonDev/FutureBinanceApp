using FutureBinanceApp.Web.Utils;
using Microsoft.Extensions.Logging;

namespace FutureBinanceApp.Web.Providers
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string Filepath { get; set; }
        public FileLoggerProvider(string filepath)
        {
            this.Filepath = filepath;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(Filepath);
        }

        public void Dispose()
        {
        }
    }
}
