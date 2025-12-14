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

        [Test]
        public void Equals_OtherNull_ReturnsFalse()
        {
            const SingletonRule<object> other = null;

            Assert.IsFalse(_singletonRule.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_SameRef_ReturnsTrue()
        {
            SingletonRule<object> other = _singletonRule;

            Assert.IsTrue(_singletonRule.Equals(other)); // Assert.AreEqual cannot be used in here
        }

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            object other = new();

            Assert.AreNotEqual(_singletonRule, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            SingletonRule<object> other = new(_ctor);

            Assert.AreEqual(_singletonRule, other);
        }

        [Test]
        public void Equals_OtherDifferentParams_ReturnsFalse()
        {
            Func<IRuleResolver, object> otherCtor = Substitute.For<Func<IRuleResolver, object>>();
            SingletonRule<object> other = new(otherCtor);

            Assert.AreNotEqual(_singletonRule, other);
        }

        [Test]
        public void GetHashCode_OtherSameParams_SameReturnedValue()
        {
            SingletonRule<object> other = new(_ctor);

            Assert.AreEqual(_singletonRule.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams_DifferentReturnedValue()
        {
            Func<IRuleResolver, object> otherCtor = Substitute.For<Func<IRuleResolver, object>>();
            SingletonRule<object> other = new(otherCtor);

            Assert.AreNotEqual(_singletonRule.GetHashCode(), other.GetHashCode());
        }
    }
}