using Microsoft.Extensions.Logging;
using System;
using ProductApi.Core.Interfaces;

namespace ProductApi.Core.Utilities {
    public class ConsoleLogger : ICustomLogger {
        private LogLevel minimumLogLevel;

        public ConsoleLogger(LogLevel minimumLogLevel) {
            this.minimumLogLevel = minimumLogLevel;
        }

        public void Log(LogLevel level, string message) {
            if (level >= minimumLogLevel) {
                Console.WriteLine($"{level.ToString().ToUpper()} | {message}");
            }
        }

        public void LogException(Exception ex, string message) {
            Console.WriteLine($"{LogLevel.Error.ToString().ToUpper()} | {message} \r\nException caught, see message for details: \r\n{ex.Message} \r\nStack Trace: {ex.StackTrace}");
        }
    }
}
