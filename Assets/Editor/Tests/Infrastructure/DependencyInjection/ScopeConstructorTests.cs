using System;
using Infrastructure.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class ScopeConstructorTests
    {
        private Action<IRuleResolver> _initialize;
        private IRuleResolver _ruleResolver;
        private IRuleAdder _ruleAdder;
        private Scope _scope;

        private ScopeConstructor _scopeConstructor;

        [SetUp]
        public void SetUp()
        {
            _initialize = Substitute.For<Action<IRuleResolver>>();
            _ruleResolver = Substitute.For<IRuleResolver>();
            _ruleAdder = Substitute.For<IRuleAdder>();
            _scope = Substitute.For<Scope>(_ruleAdder, _ruleResolver, null);

            _scopeConstructor = new ScopeConstructor();
        }

        [Test]
        public void ConstructPartial_ReturnsPartialWithValidParamsAndAddedToPartialScopes()
        {
            PartialScope partialScope = _scopeConstructor.ConstructPartial(_scope, _initialize);

            Assert.AreSame(_ruleAdder, partialScope.RuleAdder);
            Assert.AreSame(_ruleResolver, partialScope.RuleResolver);
            Assert.AreSame(_initialize, partialScope.Initialize);
            _scope.Received(1).AddPartial(partialScope);
        }

        [Test]
        public void Construct_ReturnsChildWithValidParamsAndAddedToChildScopes()
        {
            Scope childScope = _scopeConstructor.Construct(_scope, _initialize);

            Assert.AreNotSame(_ruleAdder, childScope.RuleAdder);
            Assert.AreNotSame(_ruleResolver, childScope.RuleResolver);
            Assert.AreSame(_initialize, childScope.Initialize);
            _scope.Received(1).AddChild(childScope);
        }
    }
}