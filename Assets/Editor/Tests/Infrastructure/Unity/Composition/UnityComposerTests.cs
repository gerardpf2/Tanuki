using System;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using Infrastructure.Unity;
using Infrastructure.Unity.Composition;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Unity.Composition
{
    public class UnityComposerTests
    {
        // Tested behaviours that differ from ScopeComposer

        private ScopeBuildingContext _scopeBuildingContext;
        private ICoroutineRunner _coroutineRunner;
        private IRuleFactory _ruleFactory;
        private IRuleAdder _ruleAdder;

        private UnityComposer _unityComposer;

        [SetUp]
        public void SetUp()
        {
            _coroutineRunner = Substitute.For<ICoroutineRunner>();
            _scopeBuildingContext = new ScopeBuildingContext();
            _ruleFactory = Substitute.For<IRuleFactory>();
            _ruleAdder = Substitute.For<IRuleAdder>();

            _unityComposer = new UnityComposer(_coroutineRunner);
        }

        [Test]
        public void AddRules_AddExpected()
        {
            IRule<ICoroutineRunner> coroutineRunnerRule = Substitute.For<IRule<ICoroutineRunner>>();
            IRule<IDeltaTimeGetter> deltaTimeGetterRule = Substitute.For<IRule<IDeltaTimeGetter>>();
            _ruleFactory.GetInstance(_coroutineRunner).Returns(coroutineRunnerRule);
            _ruleFactory.GetSingleton(Arg.Any<Func<IRuleResolver, IDeltaTimeGetter>>()).Returns(deltaTimeGetterRule);
            _unityComposer.Compose(_scopeBuildingContext);

            _scopeBuildingContext.AddRules(_ruleAdder, _ruleFactory);

            _ruleAdder.Received(1).Add(coroutineRunnerRule);
            _ruleAdder.Received(1).Add(deltaTimeGetterRule);
        }
    }
}