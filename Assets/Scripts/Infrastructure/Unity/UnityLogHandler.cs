using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;
using ILogHandler = Infrastructure.Logging.ILogHandler;

namespace Infrastructure.Unity
{
    public class UnityLogHandler : ILogHandler
    {
        [NotNull] private readonly ILogger _logger;

        public UnityLogHandler([NotNull] ILogger logger)
        {
            ArgumentNullException.ThrowIfNull(logger);

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