using System;
using Infrastructure.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class ScopeInitializerTests
    {
        private Action<IRuleResolver> _initialize;
        private IRuleResolver _ruleResolver;

        private ScopeInitializer _scopeInitializer;

        [SetUp]
        public void SetUp()
        {
            _initialize = Substitute.For<Action<IRuleResolver>>();
            _ruleResolver = Substitute.For<IRuleResolver>();

            _scopeInitializer = new ScopeInitializer();
        }

        [Test]
        public void Initialize_ScopeInitializeCalledWithValidParams()
        {
            Scope scope = new(null, _ruleResolver, _initialize);

            _scopeInitializer.Initialize(scope);

            _initialize.Received(1).Invoke(_ruleResolver);
        }

        [Test]
        public void Initialize_HasPartial_PartialScopeInitializeCalledWithValidParams()
        {
            Scope mainScope = new(null, _ruleResolver, null);
            PartialScope partialScope = new(mainScope, _initialize);
            mainScope.AddPartial(partialScope);

            _scopeInitializer.Initialize(mainScope);

            _initialize.Received(1).Invoke(_ruleResolver);
        }

        [Test]
        public void Initialize_HasChild_ChildScopeInitializeCalledWithValidParams()
        {
            Scope parentScope = new(null, null, null);
            Scope childScope = new(null, _ruleResolver, _initialize);
            parentScope.AddChild(childScope);

            _scopeInitializer.Initialize(parentScope);

            _initialize.Received(1).Invoke(_ruleResolver);
        }
    }
}