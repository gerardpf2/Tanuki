using System;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using Infrastructure.ScreenLoading;
using Infrastructure.ScreenLoading.Composition;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.ScreenLoading.Composition
{
    public class ScreenLoadingComposerTests
    {
        // Tested behaviours that differ from ScopeComposer

        private IScreenGetter _screenGetter;
        private ScopeBuildingContext _scopeBuildingContext;
        private IScreenPlacement _screenPlacement;
        private IRuleResolver _ruleResolver;
        private IRuleFactory _ruleFactory;
        private IRuleAdder _ruleAdder;

        private ScreenLoadingComposer _screenLoadingComposer;

        [SetUp]
        public void SetUp()
        {
            _screenGetter = Substitute.For<IScreenGetter>();
            _screenPlacement = Substitute.For<IScreenPlacement>();
            _scopeBuildingContext = new ScopeBuildingContext();
            _ruleResolver = Substitute.For<IRuleResolver>();
            _ruleFactory = Substitute.For<IRuleFactory>();
            _ruleAdder = Substitute.For<IRuleAdder>();

            _screenLoadingComposer = new ScreenLoadingComposer(_screenGetter, _screenPlacement);
        }

        [Test]
        public void AddRules_AddExpected()
        {
            IRule<IScreenGetter> screenGetterRule = Substitute.For<IRule<IScreenGetter>>();
            IRule<IScreenPlacement> screenPlacementRule = Substitute.For<IRule<IScreenPlacement>>();
            IRule<ScreenPlacementContainer> screenPlacementContainerRule = Substitute.For<IRule<ScreenPlacementContainer>>();
            IRule<IScreenPlacementAdder> screenPlacementAdderRule = Substitute.For<IRule<IScreenPlacementAdder>>();
            IRule<IScreenPlacementGetter> screenPlacementGetterRule = Substitute.For<IRule<IScreenPlacementGetter>>();
            IRule<IScreenLoader> screenLoaderRule = Substitute.For<IRule<IScreenLoader>>();
            _ruleFactory.GetInstance(_screenGetter).Returns(screenGetterRule);
            _ruleFactory.GetInstance(_screenPlacement).Returns(screenPlacementRule);
            _ruleFactory.GetSingleton(Arg.Any<Func<IRuleResolver, ScreenPlacementContainer>>()).Returns(screenPlacementContainerRule);
            _ruleFactory.GetTo<IScreenPlacementAdder, ScreenPlacementContainer>().Returns(screenPlacementAdderRule);
            _ruleFactory.GetTo<IScreenPlacementGetter, ScreenPlacementContainer>().Returns(screenPlacementGetterRule);
            _ruleFactory.GetSingleton(Arg.Any<Func<IRuleResolver, IScreenLoader>>()).Returns(screenLoaderRule);
            _screenLoadingComposer.Compose(_scopeBuildingContext);

            _scopeBuildingContext.AddRules(_ruleAdder, _ruleFactory);

            _ruleAdder.Received(1).Add(screenGetterRule);
            _ruleAdder.Received(1).Add(screenPlacementRule);
            _ruleAdder.Received(1).Add(screenPlacementContainerRule);
            _ruleAdder.Received(1).Add(screenPlacementAdderRule);
            _ruleAdder.Received(1).Add(screenPlacementGetterRule);
            _ruleAdder.Received(1).Add(screenLoaderRule);
        }

        [Test]
        public void AddSharedRules_AddExpected()
        {
            IRule<Action<ScreenPlacement>> screenPlacementRule = Substitute.For<IRule<Action<ScreenPlacement>>>();
            _ruleFactory.GetInject(Arg.Any<Action<IRuleResolver, ScreenPlacement>>()).Returns(screenPlacementRule);
            _screenLoadingComposer.Compose(_scopeBuildingContext);

            _scopeBuildingContext.AddSharedRules(_ruleAdder, _ruleFactory);

            _ruleAdder.Received(1).Add(screenPlacementRule);
        }

        [Test]
        public void Initialize_ResolveExpected()
        {
            IScreenPlacementAdder screenPlacementAdder = Substitute.For<IScreenPlacementAdder>();
            _ruleResolver.Resolve<IScreenPlacementAdder>().Returns(screenPlacementAdder);
            _ruleResolver.Resolve<IScreenPlacement>().Returns(_screenPlacement);
            _screenLoadingComposer.Compose(_scopeBuildingContext);

            _scopeBuildingContext.Initialize(_ruleResolver);

            screenPlacementAdder.Received(1).Add(_screenPlacement);
        }
    }
}