using UnityEngine;

namespace Infrastructure.Logging
{
    public class UnityLogHandler : ILogHandler
    {
        private readonly UnityEngine.ILogger _logger = Debug.unityLogger;

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