using System.Collections.Generic;
using System.Linq;
using Game.Root.Composition;
using Infrastructure.Configuring;
using Infrastructure.Configuring.Composition;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using Infrastructure.Logging.Composition;
using Infrastructure.ModelViewViewModel.Composition;
using Infrastructure.ScreenLoading;
using Infrastructure.ScreenLoading.Composition;
using Infrastructure.Tweening.Composition;
using Infrastructure.Unity;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Game.Root.Composition
{
    public class RootComposerTests
    {
        // Tested behaviours that differ from ScopeComposer

        private IScreenDefinitionGetter _screenDefinitionGetter;
        private ScopeBuildingContext _scopeBuildingContext;
        private IConfigValueGetter _configValueGetter;
        private IScreenPlacement _screenPlacement;
        private ICoroutineRunner _coroutineRunner;
        private IRuleFactory _ruleFactory;
        private IRuleAdder _ruleAdder;

        private RootComposer _rootComposer;

        [SetUp]
        public void SetUp()
        {
            _screenDefinitionGetter = Substitute.For<IScreenDefinitionGetter>();
            _configValueGetter = Substitute.For<IConfigValueGetter>();
            _screenPlacement = Substitute.For<IScreenPlacement>();
            _coroutineRunner = Substitute.For<ICoroutineRunner>();
            _scopeBuildingContext = new ScopeBuildingContext();
            _ruleFactory = Substitute.For<IRuleFactory>();
            _ruleAdder = Substitute.For<IRuleAdder>();

            _rootComposer =
                new RootComposer(
                    _screenDefinitionGetter,
                    _screenPlacement,
                    _configValueGetter,
                    _coroutineRunner
                );

            _rootComposer.Compose(_scopeBuildingContext);
        }

        [Test]
        public void AddRules_AddExpected()
        {
            IRule<ICoroutineRunner> coroutineRunnerRule = Substitute.For<IRule<ICoroutineRunner>>();
            _ruleFactory.GetInstance(_coroutineRunner).Returns(coroutineRunnerRule);

            _scopeBuildingContext.AddRules(_ruleAdder, _ruleFactory);

            _ruleAdder.Received(1).Add(coroutineRunnerRule);
        }

        [Test]
        public void GetPartialScopeComposers_ReturnsExpected()
        {
            List<IScopeComposer> partialScopeComposers = _scopeBuildingContext.GetPartialScopeComposers().ToList();

            Assert.IsTrue(partialScopeComposers.Count == 4);
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is LoggingComposer));
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is ScreenLoadingComposer));
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is ConfiguringComposer));
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is TweeningComposer));
        }

        [Test]
        public void GetChildScopeComposers_ReturnsExpected()
        {
            List<IScopeComposer> childScopeComposers = _scopeBuildingContext.GetChildScopeComposers().ToList();

            Assert.IsTrue(childScopeComposers.Count == 1);
            Assert.NotNull(childScopeComposers.Find(childScopeComposer => childScopeComposer is ModelViewViewModelComposer));
        }
    }
}