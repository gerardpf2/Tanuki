using System.Collections.Generic;
using System.Linq;
using Game.Root.Composition;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using Infrastructure.Logging.Composition;
using Infrastructure.ModelViewViewModel.Composition;
using Infrastructure.ScreenLoading;
using Infrastructure.ScreenLoading.Composition;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Game.Root.Composition
{
    public class RootComposerTests
    {
        // Tested behaviours that differ from ScopeComposer

        private IScreenDefinitionGetter _screenDefinitionGetter;
        private ScopeBuildingContext _scopeBuildingContext;
        private IScreenPlacement _screenPlacement;
        private IRuleResolver _ruleResolver;
        private IRuleFactory _ruleFactory;
        private IRuleAdder _ruleAdder;

        private RootComposer _rootComposer;

        [SetUp]
        public void SetUp()
        {
            _screenDefinitionGetter = Substitute.For<IScreenDefinitionGetter>();
            _scopeBuildingContext = new ScopeBuildingContext();
            _screenPlacement = Substitute.For<IScreenPlacement>();
            _ruleResolver = Substitute.For<IRuleResolver>();
            _ruleFactory = Substitute.For<IRuleFactory>();
            _ruleAdder = Substitute.For<IRuleAdder>();

            _rootComposer = new RootComposer(_screenDefinitionGetter, _screenPlacement);
        }

        [Test]
        public void AddPrivateRules_AddExpected()
        {
            IRule<IScreenDefinitionGetter> screenDefinitionGetterRule = Substitute.For<IRule<IScreenDefinitionGetter>>();
            IRule<IScreenPlacement> screenPlacementRule = Substitute.For<IRule<IScreenPlacement>>();
            _ruleFactory.GetInstance(_screenDefinitionGetter).Returns(screenDefinitionGetterRule);
            _ruleFactory.GetInstance(_screenPlacement).Returns(screenPlacementRule);
            _rootComposer.Compose(_scopeBuildingContext);

            _scopeBuildingContext.AddPrivateRules(_ruleAdder, _ruleFactory);

            _ruleAdder.Received(1).Add(screenDefinitionGetterRule);
            _ruleAdder.Received(1).Add(screenPlacementRule);
        }

        [Test]
        public void GetPartialScopeComposers_ReturnsExpected()
        {
            _rootComposer.Compose(_scopeBuildingContext);

            List<IScopeComposer> partialScopeComposers = _scopeBuildingContext.GetPartialScopeComposers(_ruleResolver).ToList();

            Assert.IsTrue(partialScopeComposers.Count == 2);
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is LoggingComposer));
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is ScreenLoadingComposer));
        }

        [Test]
        public void GetChildScopeComposers_ReturnsExpected()
        {
            _rootComposer.Compose(_scopeBuildingContext);

            List<IScopeComposer> childScopeComposers = _scopeBuildingContext.GetChildScopeComposers(_ruleResolver).ToList();

            Assert.IsTrue(childScopeComposers.Count == 1);
            Assert.NotNull(childScopeComposers.Find(childScopeComposer => childScopeComposer is ModelViewViewModelComposer));
        }
    }
}