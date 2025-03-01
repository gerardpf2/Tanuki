using System;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection.Rules
{
    public class TransientRuleTests
    {
        private Func<IRuleResolver, object> _ctor;
        private IRuleResolver _ruleResolver;
        private object _ctorInvokeResult;

        private TransientRule<object> _transientRule;

        [SetUp]
        public void SetUp()
        {
            _ctor = Substitute.For<Func<IRuleResolver, object>>();
            _ruleResolver = Substitute.For<IRuleResolver>();
            _ctorInvokeResult = new object();

            _transientRule = new TransientRule<object>(_ctor);

            _ctor.Invoke(_ruleResolver).Returns(_ctorInvokeResult);
        }

        [Test]
        public void Resolve_ReturnsCtorInvokeResult()
        {
            object result = _transientRule.Resolve(_ruleResolver);

            Assert.AreSame(_ctorInvokeResult, result);
        }
    }
}