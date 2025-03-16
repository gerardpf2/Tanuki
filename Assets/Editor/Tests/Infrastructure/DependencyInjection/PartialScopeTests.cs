using System;
using System.Collections.Generic;
using Infrastructure.DependencyInjection;
using NSubstitute;
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
            _mainScope = Substitute.For<Scope>(null, null, null, null, null);
        }

        [Test]
        public void PartialScopes_ThrowsException()
        {
            _partialScope = new PartialScope(_mainScope, null, null, null, null, null);

            Assert.Throws<NotSupportedException>(() => { IEnumerable<PartialScope> _ = _partialScope.PartialScopes; });
        }

        [Test]
        public void ChildScopes_ThrowsException()
        {
            _partialScope = new PartialScope(_mainScope, null, null, null, null, null);

            Assert.Throws<NotSupportedException>(() => { IEnumerable<Scope> _ = _partialScope.ChildScopes; });
        }

        [Test]
        public void AddPartial_MainScopeAddPartialCalledWithValidParams()
        {
            PartialScope otherPartialScope = new(_mainScope, null, null, null, null, null);
            _partialScope = new PartialScope(otherPartialScope, null, null, null, null, null);

            otherPartialScope.AddPartial(_partialScope);

            _mainScope.Received(1).AddPartial(_partialScope);
        }

        [Test]
        public void AddChild_MainScopeAddChildCalledWithValidParams()
        {
            Scope childScope = new(null, null, null, null, null);
            _partialScope = new PartialScope(_mainScope, null, null, null, null, null);

            _partialScope.AddChild(childScope);

            _mainScope.Received(1).AddChild(childScope);
        }
    }
}