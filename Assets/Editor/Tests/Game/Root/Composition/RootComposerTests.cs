using System.Collections.Generic;
using System.Linq;
using Game.Root.Composition;
using Infrastructure.Configuring;
using Infrastructure.Configuring.Composition;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Composition;
using Infrastructure.Logging.Composition;
using Infrastructure.ModelViewViewModel.Composition;
using Infrastructure.ScreenLoading;
using Infrastructure.ScreenLoading.Composition;
using Infrastructure.System;
using Infrastructure.Tweening.Composition;
using Infrastructure.Unity;
using Infrastructure.Unity.Composition;
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
        private IConverter _converter;

        private RootComposer _rootComposer;

        [SetUp]
        public void SetUp()
        {
            _screenDefinitionGetter = Substitute.For<IScreenDefinitionGetter>();
            _configValueGetter = Substitute.For<IConfigValueGetter>();
            _screenPlacement = Substitute.For<IScreenPlacement>();
            _coroutineRunner = Substitute.For<ICoroutineRunner>();
            _scopeBuildingContext = new ScopeBuildingContext();
            _converter = Substitute.For<IConverter>();

            _rootComposer =
                new RootComposer(
                    _screenDefinitionGetter,
                    _screenPlacement,
                    _configValueGetter,
                    _coroutineRunner,
                    _converter
                );
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
            Assert.NotNull(childScopeComposers.Find(childScopeComposer => childScopeComposer is ModelViewViewModelComposer));
        }
    }
}