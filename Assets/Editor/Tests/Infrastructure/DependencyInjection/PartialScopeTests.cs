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
        }

        [Test]
        public void PartialScope_Scope_HasValidParams()
        {
            _partialScope = new PartialScope(_mainScope, null);

            Assert.AreSame(_ruleAdder, _partialScope.RuleAdder);
            Assert.AreSame(_ruleResolver, _partialScope.RuleResolver);
        }

        [Test]
        public void AddPartial_PartialScope_AddedToMainScope()
        {
            PartialScope otherPartialScope = new(_mainScope, null);
            _mainScope.AddPartial(otherPartialScope);
            _partialScope = new PartialScope(otherPartialScope, null);

            otherPartialScope.AddPartial(_partialScope);

            Assert.IsTrue(_mainScope.PartialScopes.Count() == 2);
            Assert.IsTrue(_mainScope.PartialScopes.Contains(otherPartialScope));
            Assert.IsTrue(_mainScope.PartialScopes.Contains(_partialScope));
            Assert.IsTrue(otherPartialScope.PartialScopes.Count() == 2);
            Assert.IsTrue(otherPartialScope.PartialScopes.Contains(_mainScope));
            Assert.IsTrue(otherPartialScope.PartialScopes.Contains(_partialScope));
            Assert.IsTrue(_partialScope.PartialScopes.Count() == 2);
            Assert.IsTrue(_partialScope.PartialScopes.Contains(_mainScope));
            Assert.IsTrue(_partialScope.PartialScopes.Contains(otherPartialScope));
        }

        [Test]
        public void AddPartial_Scope_AddedToMainScope()
        {
            _partialScope = new PartialScope(_mainScope, null);

            _mainScope.AddPartial(_partialScope);

            Assert.IsTrue(_mainScope.PartialScopes.Count() == 1);
            Assert.IsTrue(_mainScope.PartialScopes.Contains(_partialScope));
            Assert.IsTrue(_partialScope.PartialScopes.Count() == 1);
            Assert.IsTrue(_partialScope.PartialScopes.Contains(_mainScope));
        }

        [Test]
        public void AddChild_PartialScope_AddedToMainScope()
        {
            Scope childScope = new(null, null, null);
            PartialScope otherPartialScope = new(_mainScope, null);
            _mainScope.AddPartial(otherPartialScope);
            _partialScope = new PartialScope(otherPartialScope, null);

            _partialScope.AddChild(childScope);

            Assert.IsTrue(_mainScope.ChildScopes.Count() == 1);
            Assert.IsTrue(_mainScope.ChildScopes.Contains(childScope));
            Assert.AreSame(_mainScope.ChildScopes, otherPartialScope.ChildScopes);
            Assert.AreSame(_mainScope.ChildScopes, _partialScope.ChildScopes);
        }

        [Test]
        public void AddChild_Scope_AddedToMainScope()
        {
            Scope childScope = new(null, null, null);
            _partialScope = new PartialScope(_mainScope, null);

            _partialScope.AddChild(childScope);

            Assert.IsTrue(_mainScope.ChildScopes.Count() == 1);
            Assert.IsTrue(_mainScope.ChildScopes.Contains(childScope));
            Assert.AreSame(_mainScope.ChildScopes, _partialScope.ChildScopes);
        }
    }
}