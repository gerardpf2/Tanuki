using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection.Rules
{
    public class TargetRuleTests
    {
        private IRuleResolver _ruleResolver;
        private object _targetResolveResult;
        private object _key;

        private TargetRule<object> _targetRule;

        [SetUp]
        public void SetUp()
        {
            _ruleResolver = Substitute.For<IRuleResolver>();
            _targetResolveResult = new object();
            _key = new object();

            _targetRule = new TargetRule<object>(_ruleResolver, _key);

            _ruleResolver.Resolve<object>(_key).Returns(_targetResolveResult);
        }

        [Test]
        public void Resolve_ReturnsTargetResolveResult()
        {
            object result = _targetRule.Resolve(null);

            Assert.AreSame(_targetResolveResult, result);
        }

        [Test]
        public void Equals_OtherNull_ReturnsFalse()
        {
            const TargetRule<object> other = null;

            Assert.IsFalse(_targetRule.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_SameRef_ReturnsTrue()
        {
            TargetRule<object> other = _targetRule;

            Assert.IsTrue(_targetRule.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            object other = new();

            Assert.AreNotEqual(_targetRule, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            TargetRule<object> other = new(_ruleResolver, _key);

            Assert.AreEqual(_targetRule, other);
        }

        [Test]
        public void Equals_OtherDifferentParams1_ReturnsFalse()
        {
            IRuleResolver otherRuleResolver = Substitute.For<IRuleResolver>();
            TargetRule<object> other = new(otherRuleResolver, _key);

            Assert.AreNotEqual(_targetRule, other);
        }

        [Test]
        public void Equals_OtherDifferentParams2_ReturnsFalse()
        {
            object otherKey = new();
            TargetRule<object> other = new(_ruleResolver, otherKey);

            Assert.AreNotEqual(_targetRule, other);
        }

        [Test]
        public void GetHashCode_SameParams_SameReturnedValue()
        {
            TargetRule<object> other = new(_ruleResolver, _key);

            Assert.AreEqual(_targetRule.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_DifferentParams1_DifferentReturnedValue()
        {
            IRuleResolver otherRuleResolver = Substitute.For<IRuleResolver>();
            TargetRule<object> other = new(otherRuleResolver, _key);

            Assert.AreNotEqual(_targetRule.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_DifferentParams2_DifferentReturnedValue()
        {
            object otherKey = new();
            TargetRule<object> other = new(_ruleResolver, otherKey);

            Assert.AreNotEqual(_targetRule.GetHashCode(), other.GetHashCode());
        }
    }
}