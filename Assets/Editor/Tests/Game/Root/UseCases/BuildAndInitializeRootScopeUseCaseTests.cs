using Game.Gameplay;
using Game.Gameplay.View.Board;
using Game.Root.UseCases;
using Infrastructure.Configuring;
using Infrastructure.DependencyInjection;
using Infrastructure.Gating;
using Infrastructure.ScreenLoading;
using Infrastructure.Unity;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Game.Root.UseCases
{
    public class BuildAndInitializeRootScopeUseCaseTests
    {
        private IPieceViewDefinitionGetter _pieceViewDefinitionGetter;
        private IScreenDefinitionGetter _screenDefinitionGetter;
        private IConfigDefinitionGetter _configDefinitionGetter;
        private IGameplayDefinitionGetter _gameplayDefinitionGetter;
        private IGateDefinitionGetter _gateDefinitionGetter;
        private IScreenPlacement _screenPlacement;
        private ICoroutineRunner _coroutineRunner;

        private BuildAndInitializeRootScopeUseCase _buildAndInitializeRootScopeUseCase;

        [SetUp]
        public void SetUp()
        {
            _pieceViewDefinitionGetter = Substitute.For<IPieceViewDefinitionGetter>();
            _screenDefinitionGetter = Substitute.For<IScreenDefinitionGetter>();
            _configDefinitionGetter = Substitute.For<IConfigDefinitionGetter>();
            _gameplayDefinitionGetter = Substitute.For<IGameplayDefinitionGetter>();
            _gateDefinitionGetter = Substitute.For<IGateDefinitionGetter>();
            _screenPlacement = Substitute.For<IScreenPlacement>();
            _coroutineRunner = Substitute.For<ICoroutineRunner>();

            _buildAndInitializeRootScopeUseCase =
                new BuildAndInitializeRootScopeUseCase(
                    _gateDefinitionGetter,
                    _configDefinitionGetter,
                    _screenDefinitionGetter,
                    _screenPlacement,
                    _coroutineRunner,
                    _gameplayDefinitionGetter,
                    _pieceViewDefinitionGetter
                );
        }

        [Test]
        public void Resolve_ReturnsNotNull()
        {
            Scope scope = _buildAndInitializeRootScopeUseCase.Resolve();

            Assert.NotNull(scope);
        }
    }
}