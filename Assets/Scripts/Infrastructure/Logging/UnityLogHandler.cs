using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Logging
{
    public class UnityLogHandler : ILogHandler
    {
        private readonly UnityEngine.ILogger _logger;

        public UnityLogHandler([NotNull] UnityEngine.ILogger logger)
        {
            _logger = logger;
        }

        public void Info(string message)
        {
            _logger.Log(LogType.Log, message);
        }

        public void Warning(string message)
        {
            _logger.Log(LogType.Warning, message);
        }

        public void Error(string message)
        {
            _logger.Log(LogType.Error, message);
        }
    }
}