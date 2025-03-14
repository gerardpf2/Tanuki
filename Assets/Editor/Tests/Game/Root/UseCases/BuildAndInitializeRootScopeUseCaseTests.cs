using Game.Root.UseCases;
using Infrastructure.DependencyInjection;
using Infrastructure.Gating;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Game.Root.UseCases
{
    public class BuildAndInitializeRootScopeUseCaseTests
    {
        private IGateDefinitionGetter _gateDefinitionGetter;

        private BuildAndInitializeRootScopeUseCase _buildAndInitializeRootScopeUseCase;

        [SetUp]
        public void SetUp()
        {
            _gateDefinitionGetter = Substitute.For<IGateDefinitionGetter>();

            _buildAndInitializeRootScopeUseCase = new BuildAndInitializeRootScopeUseCase(_gateDefinitionGetter);
        }

        [Test]
        public void Resolve_ReturnsNotNull()
        {
            Scope scope = _buildAndInitializeRootScopeUseCase.Resolve();

            Assert.NotNull(scope);
        }
    }
}