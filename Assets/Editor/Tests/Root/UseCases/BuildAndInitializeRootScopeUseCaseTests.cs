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
        private IScreenDefinitionGetter _screenDefinitionGetter;
        private IConfigDefinitionGetter _configDefinitionGetter;
        private IGateDefinitionGetter _gateDefinitionGetter;
        private IScreenPlacement _screenPlacement;
        private ICoroutineRunner _coroutineRunner;

        private BuildAndInitializeRootScopeUseCase _buildAndInitializeRootScopeUseCase;

        [SetUp]
        public void SetUp()
        {
            _screenDefinitionGetter = Substitute.For<IScreenDefinitionGetter>();
            _configDefinitionGetter = Substitute.For<IConfigDefinitionGetter>();
            _gateDefinitionGetter = Substitute.For<IGateDefinitionGetter>();
            _screenPlacement = Substitute.For<IScreenPlacement>();
            _coroutineRunner = Substitute.For<ICoroutineRunner>();

            _buildAndInitializeRootScopeUseCase =
                new BuildAndInitializeRootScopeUseCase(
                    _gateDefinitionGetter,
                    _configDefinitionGetter,
                    _screenDefinitionGetter,
                    _screenPlacement,
                    _coroutineRunner
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