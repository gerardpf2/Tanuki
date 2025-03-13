using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection.Rules
{
    public class GateKeyRuleTests
    {
        private IEnabledGateKeyGetter _enabledGateKeyGetter;
        private IRuleResolver _ruleResolver;
        private IRule<object> _rule;
        private object _ruleResult;
        private string _gateKey;

        private GateKeyRule<object> _gateKeyRule;

        [SetUp]
        public void SetUp()
        {
            _enabledGateKeyGetter = Substitute.For<IEnabledGateKeyGetter>();
            _ruleResolver = Substitute.For<IRuleResolver>();
            _rule = Substitute.For<IRule<object>>();
            _ruleResult = new object();
            _gateKey = nameof(_gateKey);

            _gateKeyRule = new GateKeyRule<object>(_enabledGateKeyGetter, _rule, _gateKey);

            _rule.Resolve(_ruleResolver).Returns(_ruleResult);
        }

        [Test]
        public void Resolve_GateKeyEnabled_ReturnsRuleResult()
        {
            _enabledGateKeyGetter.Contains(_gateKey).Returns(true);

            object result = _gateKeyRule.Resolve(_ruleResolver);

            Assert.AreSame(_ruleResult, result);
        }

        [Test]
        public void Resolve_GateKeyNotEnabled_ReturnsNull()
        {
            _enabledGateKeyGetter.Contains(_gateKey).Returns(false);

            object result = _gateKeyRule.Resolve(_ruleResolver);

            Assert.IsNull(result);
        }

        [Test]
        public void Equals_OtherNull_ReturnsFalse()
        {
            const GateKeyRule<object> other = null;

            Assert.IsFalse(_gateKeyRule.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_SameRef_ReturnsTrue()
        {
            GateKeyRule<object> other = _gateKeyRule;

            Assert.IsTrue(_gateKeyRule.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            object other = new();

            Assert.AreNotEqual(_gateKeyRule, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            GateKeyRule<object> other = new(_enabledGateKeyGetter, _rule, _gateKey);

            Assert.AreEqual(_gateKeyRule, other);
        }

        [Test]
        public void Equals_OtherDifferentParams1_ReturnsFalse()
        {
            IEnabledGateKeyGetter otherEnabledGateKeyGetter = Substitute.For<IEnabledGateKeyGetter>();
            GateKeyRule<object> other = new(otherEnabledGateKeyGetter, _rule, _gateKey);

            Assert.AreNotEqual(_gateKeyRule, other);
        }

        [Test]
        public void Equals_OtherDifferentParams2_ReturnsFalse()
        {
            IRule<object> otherRule = Substitute.For<IRule<object>>();
            GateKeyRule<object> other = new(_enabledGateKeyGetter, otherRule, _gateKey);

            Assert.AreNotEqual(_gateKeyRule, other);
        }

        [Test]
        public void Equals_OtherDifferentParams3_ReturnsFalse()
        {
            const string otherGateKey = nameof(otherGateKey);
            GateKeyRule<object> other = new(_enabledGateKeyGetter, _rule, otherGateKey);

            Assert.AreNotEqual(_gateKeyRule, other);
        }

        [Test]
        public void GetHashCode_SameParams_SameReturnedValue()
        {
            GateKeyRule<object> other = new(_enabledGateKeyGetter, _rule, _gateKey);

            Assert.AreEqual(_gateKeyRule.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_DifferentParams1_DifferentReturnedValue()
        {
            IEnabledGateKeyGetter otherEnabledGateKeyGetter = Substitute.For<IEnabledGateKeyGetter>();
            GateKeyRule<object> other = new(otherEnabledGateKeyGetter, _rule, _gateKey);

            Assert.AreNotEqual(_gateKeyRule.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_DifferentParams2_DifferentReturnedValue()
        {
            IRule<object> otherRule = Substitute.For<IRule<object>>();
            GateKeyRule<object> other = new(_enabledGateKeyGetter, otherRule, _gateKey);

            Assert.AreNotEqual(_gateKeyRule.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_DifferentParams3_DifferentReturnedValue()
        {
            const string otherGateKey = nameof(otherGateKey);
            GateKeyRule<object> other = new(_enabledGateKeyGetter, _rule, otherGateKey);

            Assert.AreNotEqual(_gateKeyRule.GetHashCode(), other.GetHashCode());
        }
    }
}