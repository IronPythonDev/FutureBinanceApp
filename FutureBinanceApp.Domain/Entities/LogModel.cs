
using Microsoft.Extensions.Logging;

namespace FutureBinanceApp.Domain.Entities
{
    public class LogModel
    {
        public int Id { get; set; }
        public string Date { get; set; }

        public LogLevel LogLevel { get; set; }

        public string Message { get; set; }
    }
}
