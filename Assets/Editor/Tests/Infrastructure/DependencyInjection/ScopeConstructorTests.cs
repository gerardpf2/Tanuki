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

        private ScopeConstructor _scopeConstructor;

        [SetUp]
        public void SetUp()
        {
            _initialize = Substitute.For<Action<IRuleResolver>>();
            _ruleResolver = Substitute.For<IRuleResolver>();
            _ruleAdder = Substitute.For<IRuleAdder>();

            _scopeConstructor = new ScopeConstructor();
        }

        [Test]
        public void ConstructPartialOf_ScopeHasValidParams()
        {
            Scope partialOfScope = new(_ruleAdder, _ruleResolver, null);

            Scope partialScope = _scopeConstructor.ConstructPartialOf(partialOfScope, _initialize);

            Assert.AreSame(_ruleAdder, partialScope.RuleAdder);
            Assert.AreSame(_ruleResolver, partialScope.RuleResolver);
            Assert.AreSame(_initialize, partialScope.Initialize);
            Assert.IsEmpty(partialScope.PartialScopes);
            Assert.IsEmpty(partialScope.ChildScopes);
        }

        [Test]
        public void ConstructChildOf_ScopeHasValidParams()
        {
            Scope parentScope = new(_ruleAdder, _ruleResolver, null);

            Scope childScope = _scopeConstructor.ConstructChildOf(parentScope, _initialize);

            Assert.AreNotSame(_ruleAdder, childScope.RuleAdder);
            Assert.AreNotSame(_ruleResolver, childScope.RuleResolver);
            Assert.AreSame(_initialize, childScope.Initialize);
            Assert.IsEmpty(childScope.PartialScopes);
            Assert.IsEmpty(childScope.ChildScopes);
        }
    }
}