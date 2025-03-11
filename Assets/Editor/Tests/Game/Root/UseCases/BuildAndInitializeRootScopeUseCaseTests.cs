using Game.Root.UseCases;
using Infrastructure.DependencyInjection;
using NUnit.Framework;

namespace Editor.Tests.Game.Root.UseCases
{
    public class BuildAndInitializeRootScopeUseCaseTests
    {
        private BuildAndInitializeRootScopeUseCase _buildAndInitializeRootScopeUseCase;

        [SetUp]
        public void SetUp()
        {
            _buildAndInitializeRootScopeUseCase = new BuildAndInitializeRootScopeUseCase();
        }

        [Test]
        public void Resolve_ReturnsNotNull()
        {
            Scope scope = _buildAndInitializeRootScopeUseCase.Resolve();

            Assert.NotNull(scope);
        }
    }
}