using System;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection.Rules
{
    public class InjectRuleTests
    {
        // Tested behaviours that differ from SingletonRule

        private Action<IRuleResolver, object> _inject;

        private InjectRule<object> _injectRule;

        [SetUp]
        public void SetUp()
        {
            _inject = Substitute.For<Action<IRuleResolver, object>>();

            _injectRule = new InjectRule<object>(_inject);
        }

        [Test]
        public void Equals_OtherNull_ReturnsFalse()
        {
            const InjectRule<object> other = null;

            Assert.IsFalse(_injectRule.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_SameRef_ReturnsTrue()
        {
            InjectRule<object> other = _injectRule;

            Assert.IsTrue(_injectRule.Equals(other)); // Assert.AreEqual cannot be used in here
        }

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            object other = new();

            Assert.AreNotEqual(_injectRule, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            InjectRule<object> other = new(_inject);

            Assert.AreEqual(_injectRule, other);
        }

        [Test]
        public void Equals_OtherDifferentParams_ReturnsFalse()
        {
            Action<IRuleResolver, object> otherInject = Substitute.For<Action<IRuleResolver, object>>();
            InjectRule<object> other = new(otherInject);

            Assert.AreNotEqual(_injectRule, other);
        }

        [Test]
        public void GetHashCode_OtherSameParams_SameReturnedValue()
        {
            InjectRule<object> other = new(_inject);

            Assert.AreEqual(_injectRule.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams_DifferentReturnedValue()
        {
            Action<IRuleResolver, object> otherInject = Substitute.For<Action<IRuleResolver, object>>();
            InjectRule<object> other = new(otherInject);

            Assert.AreNotEqual(_injectRule.GetHashCode(), other.GetHashCode());
        }
    }
}