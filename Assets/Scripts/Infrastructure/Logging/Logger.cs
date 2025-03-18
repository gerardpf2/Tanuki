using System.Collections.Generic;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Infrastructure.Logging
{
    public class Logger : ILogger
    {
        [NotNull] [ItemNotNull] private readonly ICollection<ILogHandler> _logHandlers = new List<ILogHandler>();

        public void Info(string message)
        {
            foreach (ILogHandler logHandler in _logHandlers)
            {
                logHandler.Info(message);
            }
        }

        public void Warning(string message)
        {
            foreach (ILogHandler logHandler in _logHandlers)
            {
                logHandler.Warning(message);
            }
        }

        public void Error(string message)
        {
            foreach (ILogHandler logHandler in _logHandlers)
            {
                logHandler.Error(message);
            }
        }

        public void Add([NotNull] ILogHandler logHandler)
        {
            ArgumentNullException.ThrowIfNull(logHandler);

            _logHandlers.Add(logHandler);
        }
    }
}