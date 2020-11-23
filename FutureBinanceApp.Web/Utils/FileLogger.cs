using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace FutureBinanceApp.Web.Utils
{
    public class FileLogger : ILogger
    {
        private string Filepath { get; set; }
        private static readonly object _lock = new object();
        public FileLogger(string filepath)
        {
            Filepath = filepath;
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
            if (formatter != null)
            {
                switch (logLevel)
                {
                    case LogLevel.Trace:
                        lock (_lock)
                        {
                            File.AppendAllText(Filepath + "Trace.log", $"[{DateTime.UtcNow}] => {formatter(state, exception)}" + Environment.NewLine);
                        }
                        break;
                    case LogLevel.Debug:
                        lock (_lock)
                        {
                            File.AppendAllText(Filepath + "Debug.log", $"[{DateTime.UtcNow}] => {formatter(state, exception)}" + Environment.NewLine);
                        }
                        break;
                    case LogLevel.Information:
                        lock (_lock)
                        {
                            File.AppendAllText(Filepath + "Information.log", $"[{DateTime.UtcNow}] => {formatter(state, exception)}" + Environment.NewLine);
                        }
                        break;
                    case LogLevel.Warning:
                        lock (_lock)
                        {
                            File.AppendAllText(Filepath + "Warning.log", $"[{DateTime.UtcNow}] => {formatter(state, exception)}" + Environment.NewLine);
                        }
                        break;
                    case LogLevel.Error:
                        lock (_lock)
                        {
                            File.AppendAllText(Filepath + "Error.log", $"[{DateTime.UtcNow}] => {formatter(state, exception)}" + Environment.NewLine);
                        }
                        break;
                    case LogLevel.Critical:
                        lock (_lock)
                        {
                            File.AppendAllText(Filepath + "Critical.log", $"[{DateTime.UtcNow}] => {formatter(state, exception)}" + Environment.NewLine);
                        }
                        break;
                    case LogLevel.None:
                        lock (_lock)
                        {
                            File.AppendAllText(Filepath + "None.log",  $"[{DateTime.UtcNow}] => {formatter(state, exception)}" + Environment.NewLine);
                        }
                        break;
                    default:
                        lock (_lock)
                        {
                            File.AppendAllText(Filepath + "Default.log", $"[{DateTime.UtcNow}] => {formatter(state, exception)}" + Environment.NewLine);
                        }
                        break;
                }
            }
        }
    }
}
