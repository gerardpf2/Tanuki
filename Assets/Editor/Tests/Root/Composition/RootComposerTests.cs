using System.Collections.Generic;
using System.Linq;
using Game;
using Infrastructure.Configuring;
using Infrastructure.Configuring.Composition;
using Infrastructure.DependencyInjection;
using Infrastructure.Logging.Composition;
using Infrastructure.ScreenLoading;
using Infrastructure.ScreenLoading.Composition;
using Infrastructure.System;
using Infrastructure.SystemComposition.Composition;
using Infrastructure.Tweening.Composition;
using Infrastructure.Unity;
using Infrastructure.Unity.Composition;
using NSubstitute;
using NUnit.Framework;
using Root.Composition;

namespace Editor.Tests.Root.Composition
{
    public class RootComposerTests
    {
        // Tested behaviours that differ from ScopeComposer

        private IGameScopeComposerBuilder _gameScopeComposerBuilder;
        private IScreenGetter _screenGetter;
        private ScopeBuildingContext _scopeBuildingContext;
        private IConfigValueGetter _configValueGetter;
        private IScreenPlacement _screenPlacement;
        private ICoroutineRunner _coroutineRunner;
        private IScopeComposer _gameComposer;
        private IConverter _converter;

        private RootComposer _rootComposer;

        [SetUp]
        public void SetUp()
        {
            _gameScopeComposerBuilder = Substitute.For<IGameScopeComposerBuilder>();
            _screenGetter = Substitute.For<IScreenGetter>();
            _configValueGetter = Substitute.For<IConfigValueGetter>();
            _screenPlacement = Substitute.For<IScreenPlacement>();
            _coroutineRunner = Substitute.For<ICoroutineRunner>();
            _scopeBuildingContext = new ScopeBuildingContext();
            _gameComposer = Substitute.For<IScopeComposer>();
            _converter = Substitute.For<IConverter>();

            _rootComposer =
                new RootComposer(
                    _screenGetter,
                    _screenPlacement,
                    _configValueGetter,
                    _coroutineRunner,
                    _converter,
                    _gameScopeComposerBuilder
                );

            _gameScopeComposerBuilder.Build().Returns(_gameComposer);
        }

        [Test]
        public void GetPartialScopeComposers_ReturnsExpected()
        {
            _rootComposer.Compose(_scopeBuildingContext);

            List<IScopeComposer> partialScopeComposers = _scopeBuildingContext.GetPartialScopeComposers().ToList();

            Assert.IsTrue(partialScopeComposers.Count == 6);
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is LoggingComposer));
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is ScreenLoadingComposer));
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is ConfiguringComposer));
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is TweeningComposer));
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is UnityComposer));
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is SystemComposer));
        }

        [Test]
        public void GetChildScopeComposers_ReturnsExpected()
        {
            _rootComposer.Compose(_scopeBuildingContext);

            List<IScopeComposer> childScopeComposers = _scopeBuildingContext.GetChildScopeComposers().ToList();

            Assert.IsTrue(childScopeComposers.Count == 1);
            Assert.IsTrue(childScopeComposers.Contains(_gameComposer));
        }
    }
}