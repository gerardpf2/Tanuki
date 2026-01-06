using Game;
using Infrastructure.Configuring;
using Infrastructure.DependencyInjection;
using Infrastructure.Gating;
using Infrastructure.ScreenLoading;
using Infrastructure.Unity;
using NSubstitute;
using NUnit.Framework;
using Root.UseCases;

namespace Editor.Tests.Root.UseCases
{
    public class BuildAndInitializeRootScopeUseCaseTests
    {
        private IGameScopeComposerBuilder _gameScopeComposerBuilder;
        private IScreenGetter _screenGetter;
        private IConfigDefinitionGetter _configDefinitionGetter;
        private IGateDefinitionGetter _gateDefinitionGetter;
        private IScreenPlacement _screenPlacement;
        private ICoroutineRunner _coroutineRunner;

        private BuildAndInitializeRootScopeUseCase _buildAndInitializeRootScopeUseCase;

        [SetUp]
        public void SetUp()
        {
            _gameScopeComposerBuilder = Substitute.For<IGameScopeComposerBuilder>();
            _screenGetter = Substitute.For<IScreenGetter>();
            _configDefinitionGetter = Substitute.For<IConfigDefinitionGetter>();
            _gateDefinitionGetter = Substitute.For<IGateDefinitionGetter>();
            _screenPlacement = Substitute.For<IScreenPlacement>();
            _coroutineRunner = Substitute.For<ICoroutineRunner>();

            _buildAndInitializeRootScopeUseCase =
                new BuildAndInitializeRootScopeUseCase(
                    _gateDefinitionGetter,
                    _configDefinitionGetter,
                    _screenGetter,
                    _screenPlacement,
                    _coroutineRunner,
                    _gameScopeComposerBuilder
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