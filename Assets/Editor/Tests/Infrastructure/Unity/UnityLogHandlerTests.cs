using Infrastructure.Unity;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests.Infrastructure.Unity
{
    public class UnityLogHandlerTests
    {
        private ILogger _logger;
        private string _message;

        private UnityLogHandler _unityLogHandler;

        [SetUp]
        public void SetUp()
        {
            _logger = Substitute.For<ILogger>();
            _message = nameof(_message);

            _unityLogHandler = new UnityLogHandler(_logger);
        }

        [Test]
        public void Info_UnityLoggerLogIsCalledWithValidParams()
        {
            _unityLogHandler.Info(_message);

            _logger.Received(1).Log(LogType.Log, _message);
        }

        [Test]
        public void Warning_UnityLoggerLogIsCalledWithValidParams()
        {
            _unityLogHandler.Warning(_message);

            _logger.Received(1).Log(LogType.Warning, _message);
        }

        [Test]
        public void Error_UnityLoggerLogIsCalledWithValidParams()
        {
            _unityLogHandler.Error(_message);

            _logger.Received(1).Log(LogType.Error, _message);
        }
    }
}