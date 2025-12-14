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

        [Test]
        public void Equals_OtherNull_ReturnsFalse()
        {
            const TransientRule<object> other = null;

            Assert.IsFalse(_transientRule.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_SameRef_ReturnsTrue()
        {
            TransientRule<object> other = _transientRule;

            Assert.IsTrue(_transientRule.Equals(other)); // Assert.AreEqual cannot be used in here
        }

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            object other = new();

            Assert.AreNotEqual(_transientRule, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            TransientRule<object> other = new(_ctor);

            Assert.AreEqual(_transientRule, other);
        }

        [Test]
        public void Equals_OtherDifferentParams_ReturnsFalse()
        {
            Func<IRuleResolver, object> otherCtor = Substitute.For<Func<IRuleResolver, object>>();
            TransientRule<object> other = new(otherCtor);

            Assert.AreNotEqual(_transientRule, other);
        }

        [Test]
        public void GetHashCode_OtherSameParams_SameReturnedValue()
        {
            TransientRule<object> other = new(_ctor);

            Assert.AreEqual(_transientRule.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams_DifferentReturnedValue()
        {
            Func<IRuleResolver, object> otherCtor = Substitute.For<Func<IRuleResolver, object>>();
            TransientRule<object> other = new(otherCtor);

            Assert.AreNotEqual(_transientRule.GetHashCode(), other.GetHashCode());
        }
    }
}