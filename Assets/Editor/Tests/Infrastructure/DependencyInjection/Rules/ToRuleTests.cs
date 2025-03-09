using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection.Rules
{
    public class ToRuleTests
    {
        private IRuleResolver _ruleResolver;
        private object _toResolveResult;
        private object _key;

        private ToRule<object, object> _toRule;

        [SetUp]
        public void SetUp()
        {
            _ruleResolver = Substitute.For<IRuleResolver>();
            _toResolveResult = new object();
            _key = new object();

            _toRule = new ToRule<object, object>(_key);

            _ruleResolver.Resolve<object>(_key).Returns(_toResolveResult);
        }

        [Test]
        public void Resolve_ReturnsToResolveResult()
        {
            object result = _toRule.Resolve(_ruleResolver);

            Assert.AreSame(_toResolveResult, result);
        }

        [Test]
        public void Equals_OtherNull_ReturnsFalse()
        {
            const ToRule<object, object> other = null;

            Assert.IsFalse(_toRule.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_SameRef_ReturnsTrue()
        {
            ToRule<object, object> other = _toRule;

            Assert.IsTrue(_toRule.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            object other = new();

            Assert.AreNotEqual(_toRule, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            ToRule<object, object> other = new(_key);

            Assert.AreEqual(_toRule, other);
        }

        [Test]
        public void Equals_OtherDifferentParams_ReturnsFalse()
        {
            object otherKey = new();
            ToRule<object, object> other = new(otherKey);

            Assert.AreNotEqual(_toRule, other);
        }

        [Test]
        public void GetHashCode_SameParams_SameReturnedValue()
        {
            ToRule<object, object> other = new(_key);

            Assert.AreEqual(_toRule.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_DifferentParams_DifferentReturnedValue()
        {
            object otherKey = new();
            ToRule<object, object> other = new(otherKey);

            Assert.AreNotEqual(_toRule.GetHashCode(), other.GetHashCode());
        }
    }
}