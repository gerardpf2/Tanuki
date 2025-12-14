namespace Infrastructure.Logging
{
    public interface ILogHandler
    {
        void Info(string message);

        void Warning(string message);

        void Error(string message);

        void Info(IComposedMessage composedMessage);

        void Warning(IComposedMessage composedMessage);

        void Error(IComposedMessage composedMessage);
    }
}