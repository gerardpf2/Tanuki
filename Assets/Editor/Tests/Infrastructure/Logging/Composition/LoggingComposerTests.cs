using System;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using Infrastructure.Logging;
using Infrastructure.Logging.Composition;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Logging.Composition
{
    public class LoggingComposerTests
    {
        // Tested behaviours that differ from ScopeComposer

        private ScopeBuildingContext _scopeBuildingContext;
        private IRuleResolver _ruleResolver;
        private IRuleFactory _ruleFactory;
        private IRuleAdder _ruleAdder;

        private LoggingComposer _loggingComposer;

        [SetUp]
        public void SetUp()
        {
            _scopeBuildingContext = new ScopeBuildingContext();
            _ruleResolver = Substitute.For<IRuleResolver>();
            _ruleFactory = Substitute.For<IRuleFactory>();
            _ruleAdder = Substitute.For<IRuleAdder>();

            _loggingComposer = new LoggingComposer();
        }

        [Test]
        public void AddPrivateRules_AddExpected()
        {
            IRule<UnityLogHandler> unityLogHandlerRule = Substitute.For<IRule<UnityLogHandler>>();
            _ruleFactory.GetSingleton(Arg.Any<Func<IRuleResolver, UnityLogHandler>>()).Returns(unityLogHandlerRule);
            _loggingComposer.Compose(_scopeBuildingContext);

            _scopeBuildingContext.AddPrivateRules(_ruleAdder, _ruleFactory);

            _ruleAdder.Received(1).Add(unityLogHandlerRule);
        }

        [Test]
        public void AddPublicRules_AddExpected()
        {
            IRule<ILogger> loggerRule = Substitute.For<IRule<ILogger>>();
            _ruleFactory.GetSingleton(Arg.Any<Func<IRuleResolver, ILogger>>()).Returns(loggerRule);
            _loggingComposer.Compose(_scopeBuildingContext);

            _scopeBuildingContext.AddPublicRules(_ruleAdder, _ruleFactory);

            _ruleAdder.Received(1).Add(loggerRule);
        }

        [Test]
        public void Initialize_ResolveExpected()
        {
            ILogger logger = Substitute.For<ILogger>();
            UnityEngine.ILogger unityLogger = Substitute.For<UnityEngine.ILogger>();
            UnityLogHandler unityLogHandler = Substitute.For<UnityLogHandler>(unityLogger);
            _ruleResolver.Resolve<ILogger>().Returns(logger);
            _ruleResolver.Resolve<UnityLogHandler>().Returns(unityLogHandler);
            _loggingComposer.Compose(_scopeBuildingContext);

            _scopeBuildingContext.Initialize(_ruleResolver);

            logger.Received(1).Add(unityLogHandler);
        }
    }
}