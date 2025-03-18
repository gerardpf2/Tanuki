using Game.Root.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.Gating;
using Infrastructure.ScreenLoading;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Game.Root.UseCases
{
    public class BuildAndInitializeRootScopeUseCaseTests
    {
        private IScreenDefinitionGetter _screenDefinitionGetter;
        private IGateDefinitionGetter _gateDefinitionGetter;
        private IScreenPlacement _screenPlacement;

        private BuildAndInitializeRootScopeUseCase _buildAndInitializeRootScopeUseCase;

        [SetUp]
        public void SetUp()
        {
            _screenDefinitionGetter = Substitute.For<IScreenDefinitionGetter>();
            _gateDefinitionGetter = Substitute.For<IGateDefinitionGetter>();
            _screenPlacement = Substitute.For<IScreenPlacement>();

            _buildAndInitializeRootScopeUseCase =
                new BuildAndInitializeRootScopeUseCase(
                    _gateDefinitionGetter,
                    _screenDefinitionGetter,
                    _screenPlacement
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