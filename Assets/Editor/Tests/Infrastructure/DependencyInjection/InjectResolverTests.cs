using System;
using Infrastructure.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class InjectResolverTests
    {
        private IRuleResolver _ruleResolver;
        private Action<object> _inject;
        private object _instance;
        private object _key;

        [SetUp]
        public void SetUp()
        {
            _ruleResolver = Substitute.For<IRuleResolver>();
            _inject = Substitute.For<Action<object>>();
            _instance = new object();
            _key = new object();

            _ruleResolver.Resolve<Action<object>>(_key).Returns(_inject);
        }

        [Test]
        public void Resolve_RuleResolverResolveCalledWithValidParams()
        {
            InjectResolver _ = new(_ruleResolver);

            InjectResolver.Resolve(_instance, _key);

            _inject.Received(1).Invoke(_instance);
        }
    }
}