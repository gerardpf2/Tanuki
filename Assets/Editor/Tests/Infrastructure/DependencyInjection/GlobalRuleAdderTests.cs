using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class GlobalRuleAdderTests
    {
        private IRuleResolver _targetRuleResolver;
        private IRule<object> _ruleFactoryResult;
        private IRuleAdder _targetRuleAdder;
        private IRuleFactory _ruleFactory;
        private IRuleAdder _ruleAdder;
        private IRule<object> _rule;
        private object _key;

        private GlobalRuleAdder _globalRuleAdder;

        [SetUp]
        public void SetUp()
        {
            _targetRuleResolver = Substitute.For<IRuleResolver>();
            _ruleFactoryResult = Substitute.For<IRule<object>>();
            _targetRuleAdder = Substitute.For<IRuleAdder>();
            _ruleFactory = Substitute.For<IRuleFactory>();
            _ruleAdder = Substitute.For<IRuleAdder>();
            _rule = Substitute.For<IRule<object>>();
            _key = new object();

            _globalRuleAdder = new GlobalRuleAdder(_ruleAdder, _ruleFactory);

            _ruleFactory.GetTarget<object>(_targetRuleResolver, _key).Returns(_ruleFactoryResult);
        }

        [Test]
        public void Add_SetTarget_RuleAdderAndTargetRuleAdderAreCalledWithValidParams()
        {
            _globalRuleAdder.SetTarget(_targetRuleAdder, _targetRuleResolver);

            _globalRuleAdder.Add(_rule, _key);

            _ruleAdder.Received(1).Add(_ruleFactoryResult, _key);
            _targetRuleAdder.Received(1).Add(_rule, _key);
        }
    }
}