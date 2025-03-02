using System;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection.Rules
{
    public class SingletonRuleTests
    {
        private Func<IRuleResolver, object> _ctor;
        private IRuleResolver _ruleResolver;
        private object _ctorInvokeResult;

        private SingletonRule<object> _singletonRule;

        [SetUp]
        public void SetUp()
        {
            _ctor = Substitute.For<Func<IRuleResolver, object>>();
            _ruleResolver = Substitute.For<IRuleResolver>();
            _ctorInvokeResult = new object();

            _singletonRule = new SingletonRule<object>(_ctor);

            _ctor.Invoke(_ruleResolver).Returns(_ctorInvokeResult);
        }

        [Test]
        public void Resolve_ReturnsCtorInvokeResult()
        {
            object result = _singletonRule.Resolve(_ruleResolver);

            Assert.AreSame(_ctorInvokeResult, result);
        }

        [Test]
        public void Resolve_ResolveCalledMultipleTimes_CtorInvokedOneTime()
        {
            const int resolveCalledTimes = 5;

            for (int i = 0; i < resolveCalledTimes; ++i)
            {
                _singletonRule.Resolve(_ruleResolver);
            }

            _ctor.Received(1).Invoke(_ruleResolver);
        }
    }
}