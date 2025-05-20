using System.Collections.Generic;
using System.Linq;
using Game.Composition;
using Game.Gameplay.Board;
using Game.Gameplay.View.Board;
using Game.Root.Composition;
using Infrastructure.Configuring;
using Infrastructure.Configuring.Composition;
using Infrastructure.DependencyInjection;
using Infrastructure.Logging.Composition;
using Infrastructure.ModelViewViewModel.Composition;
using Infrastructure.ScreenLoading;
using Infrastructure.ScreenLoading.Composition;
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

        private IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        private IScreenDefinitionGetter _screenDefinitionGetter;
        private IBoardDefinitionGetter _boardDefinitionGetter;
        private ScopeBuildingContext _scopeBuildingContext;
        private IConfigValueGetter _configValueGetter;
        private IScreenPlacement _screenPlacement;
        private ICoroutineRunner _coroutineRunner;

        private RootComposer _rootComposer;

        [SetUp]
        public void SetUp()
        {
            _pieceViewDefinitionGetter = Substitute.For<IPieceViewDefinitionGetter>();
            _screenDefinitionGetter = Substitute.For<IScreenDefinitionGetter>();
            _boardDefinitionGetter = Substitute.For<IBoardDefinitionGetter>();
            _configValueGetter = Substitute.For<IConfigValueGetter>();
            _screenPlacement = Substitute.For<IScreenPlacement>();
            _coroutineRunner = Substitute.For<ICoroutineRunner>();
            _scopeBuildingContext = new ScopeBuildingContext();

            _rootComposer =
                new RootComposer(
                    _screenDefinitionGetter,
                    _screenPlacement,
                    _configValueGetter,
                    _coroutineRunner,
                    _boardDefinitionGetter,
                    _pieceViewDefinitionGetter
                );

            _rootComposer.Compose(_scopeBuildingContext);
        }

        [Test]
        public void GetPartialScopeComposers_ReturnsExpected()
        {
            List<IScopeComposer> partialScopeComposers = _scopeBuildingContext.GetPartialScopeComposers().ToList();

            Assert.IsTrue(partialScopeComposers.Count == 5);
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is LoggingComposer));
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is ScreenLoadingComposer));
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is ConfiguringComposer));
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is TweeningComposer));
            Assert.NotNull(partialScopeComposers.Find(partialScopeComposer => partialScopeComposer is UnityComposer));
        }

        [Test]
        public void GetChildScopeComposers_ReturnsExpected()
        {
            List<IScopeComposer> childScopeComposers = _scopeBuildingContext.GetChildScopeComposers().ToList();

            Assert.IsTrue(childScopeComposers.Count == 2);
            Assert.NotNull(childScopeComposers.Find(childScopeComposer => childScopeComposer is ModelViewViewModelComposer));
            Assert.NotNull(childScopeComposers.Find(childScopeComposer => childScopeComposer is GameComposer));
        }
    }
}