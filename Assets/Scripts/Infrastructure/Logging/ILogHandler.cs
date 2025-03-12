namespace Infrastructure.Logging
{
    public interface ILogHandler
    {
        void Info(string message);

        void Warning(string message);

        void Error(string message);
    }
}