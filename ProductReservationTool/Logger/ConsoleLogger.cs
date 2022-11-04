using ProductReservationTool.Domain.Interfaces;
using System.Reflection;

namespace ProductReservationTool.Logger
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger(LogLevel logLevel) : base(logLevel) { }

        public override void LogError(Exception ex)
        {
            Console.WriteLine("Exception : " + ex.Message);
        }

        public override void LogInfo(string message)
        {
            if (logLevel == LogLevel.Error || logLevel == LogLevel.Warning)
                return;

            Console.WriteLine("Info : " + message);
        }

        public override void LogWarning(string message)
        {
            if (logLevel == LogLevel.Error)
                return;

            Console.WriteLine("Warning : " + message);
        }
    }
}
