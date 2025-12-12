using System;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using Infrastructure.System;
using Infrastructure.System.Parsing;
using Infrastructure.SystemComposition.Composition;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.SystemComposition.Composition
{
    public class SystemComposerTests
    {
        // Tested behaviours that differ from ScopeComposer

        private ScopeBuildingContext _scopeBuildingContext;
        private IRuleFactory _ruleFactory;
        private IRuleAdder _ruleAdder;
        private IConverter _converter;

        private SystemComposer _systemComposer;

        [SetUp]
        public void SetUp()
        {
            _scopeBuildingContext = new ScopeBuildingContext();
            _ruleFactory = Substitute.For<IRuleFactory>();
            _ruleAdder = Substitute.For<IRuleAdder>();
            _converter = Substitute.For<IConverter>();

            _systemComposer = new SystemComposer(_converter);
        }

        [Test]
        public void AddRules_AddExpected()
        {
            IRule<IParser> parserRule = Substitute.For<IRule<IParser>>();
            IRule<IConverter> converterRule = Substitute.For<IRule<IConverter>>();
            _ruleFactory.GetSingleton(Arg.Any<Func<IRuleResolver, IParser>>()).Returns(parserRule);
            _ruleFactory.GetInstance(_converter).Returns(converterRule);
            _systemComposer.Compose(_scopeBuildingContext);

            _scopeBuildingContext.AddRules(_ruleAdder, _ruleFactory);

            _ruleAdder.Received(1).Add(parserRule);
            _ruleAdder.Received(1).Add(converterRule);
        }
    }
}