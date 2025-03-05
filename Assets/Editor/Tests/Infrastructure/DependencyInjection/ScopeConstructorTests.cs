using System.Linq;
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
            Scope mainScope = new(null, null, null);

            _scopeConstructor.ConstructPartial(mainScope, null);

            Assert.IsTrue(mainScope.PartialScopes.Count() == 1);
        }

        [Test]
        public void Construct_ChildAdded()
        {
            Scope parentScope = new(null, null, null);

            _scopeConstructor.Construct(parentScope, null);

            Assert.IsTrue(parentScope.ChildScopes.Count() == 1);
        }
    }
}