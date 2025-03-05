using Infrastructure.DependencyInjection;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class ScopeConstructorTests
    {
        private ScopeConstructor _scopeConstructor;

        [SetUp]
        public void SetUp()
        {
            _scopeConstructor = new ScopeConstructor();
        }

        [Test]
        public void ConstructPartial_PartialAdded()
        {
            // TODO
        }

        [Test]
        public void Construct_ChildAdded()
        {
            // TODO
        }
    }
}