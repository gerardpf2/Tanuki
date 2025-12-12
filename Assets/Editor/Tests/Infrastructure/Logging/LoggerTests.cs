using Infrastructure.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Logging
{
    public class LoggerTests
    {
        private ILogHandler _logHandler1;
        private ILogHandler _logHandler2;
        private string _message;

        private Logger _logger;

        [SetUp]
        public void SetUp()
        {
            _logHandler1 = Substitute.For<ILogHandler>();
            _logHandler2 = Substitute.For<ILogHandler>();
            _message = nameof(_message);

            _logger = new Logger();

            _logger.Add(_logHandler1);
            _logger.Add(_logHandler2);
        }

        [Test]
        public void Info_LogHandlersInfoIsCalledWithValidParams()
        {
            _logger.Info(_message);

            _logHandler1.Received(1).Info(_message);
            _logHandler2.Received(1).Info(_message);
        }

        [Test]
        public void Warning_LogHandlersWarningIsCalledWithValidParams()
        {
            _logger.Warning(_message);

            _logHandler1.Received(1).Warning(_message);
            _logHandler2.Received(1).Warning(_message);
        }

        [Test]
        public void Error_LogHandlersErrorIsCalledWithValidParams()
        {
            _logger.Error(_message);

            _logHandler1.Received(1).Error(_message);
            _logHandler2.Received(1).Error(_message);
        }
    }
}