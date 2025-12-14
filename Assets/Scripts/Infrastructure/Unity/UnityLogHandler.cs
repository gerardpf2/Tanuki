using System.Text;
using Infrastructure.Logging;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;
using UnityEngine;
using ILogger = UnityEngine.ILogger;
using ILogHandler = Infrastructure.Logging.ILogHandler;

namespace Infrastructure.Unity
{
    public class UnityLogHandler : ILogHandler
    {
        [NotNull] private readonly ILogger _logger;

        [NotNull] private readonly StringBuilder _stringBuilder = new();

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

        public void Info([NotNull] IComposedMessage composedMessage)
        {
            ArgumentNullException.ThrowIfNull(composedMessage);

            Info(GetMessage(composedMessage));
        }

        public void Warning([NotNull] IComposedMessage composedMessage)
        {
            ArgumentNullException.ThrowIfNull(composedMessage);

            Warning(GetMessage(composedMessage));
        }

        public void Error([NotNull] IComposedMessage composedMessage)
        {
            ArgumentNullException.ThrowIfNull(composedMessage);

            Error(GetMessage(composedMessage));
        }

        private string GetMessage([NotNull] IComposedMessage composedMessage)
        {
            ArgumentNullException.ThrowIfNull(composedMessage);

            _stringBuilder.Clear();

            _stringBuilder.AppendLine(composedMessage.Body);

            foreach ((string key, string value) in composedMessage.Dimensions)
            {
                _stringBuilder.AppendLine($"({key}: {value})");
            }

            return _stringBuilder.ToString();
        }
    }
}