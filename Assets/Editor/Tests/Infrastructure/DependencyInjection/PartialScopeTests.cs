using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class PartialScopeTests
    {
        // Tested behaviours that differ from Scope

        private IRuleResolver _ruleResolver;
        private IRuleAdder _ruleAdder;
        private Scope _mainScope;

        private PartialScope _partialScope;

        [SetUp]
        public void SetUp()
        {
            _ruleResolver = Substitute.For<IRuleResolver>();
            _ruleAdder = Substitute.For<IRuleAdder>();
            _mainScope = new Scope(_ruleAdder, _ruleResolver, null);
        }

        [Test]
        public void PartialScope_PartialScope_HasValidParams()
        {
            PartialScope otherPartialScope = new(_mainScope, null);

            _partialScope = new PartialScope(otherPartialScope, null);

            Assert.AreSame(_ruleAdder, _partialScope.RuleAdder);
            Assert.AreSame(_ruleResolver, _partialScope.RuleResolver);
            Assert.AreSame(_mainScope, _partialScope.MainScope);
        }

        [Test]
        public void PartialScope_Scope_HasValidParams()
        {
            _partialScope = new PartialScope(_mainScope, null);

            Assert.AreSame(_ruleAdder, _partialScope.RuleAdder);
            Assert.AreSame(_ruleResolver, _partialScope.RuleResolver);
            Assert.AreSame(_mainScope, _partialScope.MainScope);
        }

        [Test]
        public void PartialScopes_ThrowsException()
        {
            _partialScope = new PartialScope(_mainScope, null);

            NotSupportedException invalidOperationException = Assert.Throws<NotSupportedException>(() => { IEnumerable<PartialScope> _ = _partialScope.PartialScopes; });
            Assert.AreEqual($"Use {nameof(_partialScope.MainScope)}.{nameof(_partialScope.MainScope.PartialScopes)} instead", invalidOperationException.Message);
        }

        [Test]
        public void ChildScopes_ThrowsException()
        {
            _partialScope = new PartialScope(_mainScope, null);

            NotSupportedException invalidOperationException = Assert.Throws<NotSupportedException>(() => { IEnumerable<Scope> _ = _partialScope.ChildScopes; });
            Assert.AreEqual($"Use {nameof(_partialScope.MainScope)}.{nameof(_partialScope.MainScope.ChildScopes)} instead", invalidOperationException.Message);
        }

        [Test]
        public void AddPartial_AddedToMainScope()
        {
            PartialScope otherPartialScope = new(_mainScope, null);
            _partialScope = new PartialScope(otherPartialScope, null);

            otherPartialScope.AddPartial(_partialScope);

            Assert.IsTrue(_mainScope.PartialScopes.Count() == 1);
            Assert.IsTrue(_mainScope.PartialScopes.Contains(_partialScope));
        }

        [Test]
        public void AddChild_AddedToMainScope()
        {
            Scope childScope = new(null, null, null);
            _partialScope = new PartialScope(_mainScope, null);

            _partialScope.AddChild(childScope);

            Assert.IsTrue(_mainScope.ChildScopes.Count() == 1);
            Assert.IsTrue(_mainScope.ChildScopes.Contains(childScope));
        }
    }
}