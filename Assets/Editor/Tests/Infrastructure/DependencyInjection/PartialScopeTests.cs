using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.DependencyInjection;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class PartialScopeTests
    {
        // Tested behaviours that differ from Scope

        private Scope _mainScope;

        private PartialScope _partialScope;

        [SetUp]
        public void SetUp()
        {
            _mainScope = new Scope(null, null, null);
        }

        [Test]
        public void PartialScopes_ThrowsException()
        {
            _partialScope = new PartialScope(_mainScope, null, null, null);

            Assert.Throws<NotSupportedException>(() => { IEnumerable<PartialScope> _ = _partialScope.PartialScopes; });
        }

        [Test]
        public void ChildScopes_ThrowsException()
        {
            _partialScope = new PartialScope(_mainScope, null, null, null);

            Assert.Throws<NotSupportedException>(() => { IEnumerable<Scope> _ = _partialScope.ChildScopes; });
        }

        [Test]
        public void AddPartial_AddedToMainScope()
        {
            PartialScope otherPartialScope = new(_mainScope, null, null, null);
            _partialScope = new PartialScope(otherPartialScope, null, null, null);

            otherPartialScope.AddPartial(_partialScope);

            Assert.IsTrue(_mainScope.PartialScopes.Count() == 1);
            Assert.IsTrue(_mainScope.PartialScopes.Contains(_partialScope));
        }

        [Test]
        public void AddChild_AddedToMainScope()
        {
            Scope childScope = new(null, null, null);
            _partialScope = new PartialScope(_mainScope, null, null, null);

            _partialScope.AddChild(childScope);

            Assert.IsTrue(_mainScope.ChildScopes.Count() == 1);
            Assert.IsTrue(_mainScope.ChildScopes.Contains(childScope));
        }
    }
}