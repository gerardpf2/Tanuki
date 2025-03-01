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
        private object _gateKey;

        private GateKeyRule<object> _gateKeyRule;

        [SetUp]
        public void SetUp()
        {
            _enabledGateKeyGetter = Substitute.For<IEnabledGateKeyGetter>();
            _ruleResolver = Substitute.For<IRuleResolver>();
            _rule = Substitute.For<IRule<object>>();
            _ruleResult = new object();
            _gateKey = new object();

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
    }
}