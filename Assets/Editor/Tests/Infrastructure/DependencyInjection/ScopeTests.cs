using System;
using System.Linq;
using Infrastructure.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class ScopeTests
    {
        private Action<IRuleResolver> _initialize;
        private IRuleAdder _privateRuleAdder;
        private IRuleResolver _ruleResolver;
        private IRuleAdder _publicRuleAdder;

        private Scope _scope;

        [SetUp]
        public void SetUp()
        {
            _initialize = Substitute.For<Action<IRuleResolver>>();
            _privateRuleAdder = Substitute.For<IRuleAdder>();
            _ruleResolver = Substitute.For<IRuleResolver>();
            _publicRuleAdder = Substitute.For<IRuleAdder>();

            _scope = new Scope(_privateRuleAdder, _publicRuleAdder, _ruleResolver, _initialize);
        }

        [Test]
        public void Scope_HasValidParams()
        {
            Assert.AreSame(_privateRuleAdder, _scope.PrivateRuleAdder);
            Assert.AreSame(_publicRuleAdder, _scope.PublicRuleAdder);
            Assert.AreSame(_ruleResolver, _scope.RuleResolver);
            Assert.AreSame(_initialize, _scope.Initialize);
            Assert.IsEmpty(_scope.PartialScopes);
            Assert.IsEmpty(_scope.ChildScopes);
        }

        [Test]
        public void AddPartial_One_OneAdded()
        {
            PartialScope partialScope = new(_scope, null, null, null, null);

            _scope.AddPartial(partialScope);

            Assert.IsTrue(_scope.PartialScopes.Count() == 1);
            Assert.IsTrue(_scope.PartialScopes.Contains(partialScope));
        }

        [Test]
        public void AddPartial_Multiple_MultipleAdded()
        {
            PartialScope partialScope1 = new(_scope, null, null, null, null);
            PartialScope partialScope2 = new(_scope, null, null, null, null);

            _scope.AddPartial(partialScope1);
            _scope.AddPartial(partialScope2);

            Assert.IsTrue(_scope.PartialScopes.Count() == 2);
            Assert.IsTrue(_scope.PartialScopes.Contains(partialScope1));
            Assert.IsTrue(_scope.PartialScopes.Contains(partialScope2));
        }

        [Test]
        public void AddPartial_MultipleDuplicated_OneAdded()
        {
            PartialScope partialScope = new(_scope, null, null, null, null);

            _scope.AddPartial(partialScope);
            _scope.AddPartial(partialScope);

            Assert.IsTrue(_scope.PartialScopes.Count() == 1);
            Assert.IsTrue(_scope.PartialScopes.Contains(partialScope));
        }

        [Test]
        public void AddChild_One_OneAdded()
        {
            Scope childScope = new(null, null, null, null);

            _scope.AddChild(childScope);

            Assert.IsTrue(_scope.ChildScopes.Count() == 1);
            Assert.IsTrue(_scope.ChildScopes.Contains(childScope));
        }

        [Test]
        public void AddChild_Multiple_MultipleAdded()
        {
            Scope childScope1 = new(null, null, null, null);
            Scope childScope2 = new(null, null, null, null);

            _scope.AddChild(childScope1);
            _scope.AddChild(childScope2);

            Assert.IsTrue(_scope.ChildScopes.Count() == 2);
            Assert.IsTrue(_scope.ChildScopes.Contains(childScope1));
            Assert.IsTrue(_scope.ChildScopes.Contains(childScope2));
        }

        [Test]
        public void AddChild_MultipleDuplicated_OneAdded()
        {
            Scope childScope = new(null, null, null, null);

            _scope.AddChild(childScope);
            _scope.AddChild(childScope);

            Assert.IsTrue(_scope.ChildScopes.Count() == 1);
            Assert.IsTrue(_scope.ChildScopes.Contains(childScope));
        }
    }
}