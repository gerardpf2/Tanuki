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
        private IRuleFactory _ruleFactory;
        private IRuleAdder _ruleAdder;

        private LoggingComposer _loggingComposer;

        [SetUp]
        public void SetUp()
        {
            _scopeBuildingContext = new ScopeBuildingContext();
            _ruleFactory = Substitute.For<IRuleFactory>();
            _ruleAdder = Substitute.For<IRuleAdder>();

            _loggingComposer = new LoggingComposer();
        }

        [Test]
        public void AddRules_AddExpected()
        {
            IRule<ILogger> loggerRule = Substitute.For<IRule<ILogger>>();
            _ruleFactory.GetSingleton(Arg.Any<Func<IRuleResolver, ILogger>>()).Returns(loggerRule);
            _loggingComposer.Compose(_scopeBuildingContext);

            _scopeBuildingContext.AddRules(_ruleAdder, _ruleFactory);

            _ruleAdder.Received(1).Add(loggerRule);
        }
    }
}