using Infrastructure.Logging.LogHandlers;

namespace Infrastructure.Logging
{
    public interface ILogger : ILogHandler
    {
        void Add(ILogHandler logHandler);
    }
}