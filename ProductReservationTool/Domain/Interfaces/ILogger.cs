using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.Domain.Interfaces
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }

    public abstract class ILogger
    {
        protected LogLevel logLevel;

        public ILogger(LogLevel logLevel)
        {
            this.logLevel = logLevel;
        }

        public abstract void LogError(Exception ex);
        public abstract void LogWarning(string message);
        public abstract void LogInfo(string message);
    }
}
