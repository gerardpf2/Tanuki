using System;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class RuleFactoryTests
    {
        private IEnabledGateKeyGetter _enabledGateKeyGetter;

        private RuleFactory _ruleFactory;

        [SetUp]
        public void SetUp()
        {
            _enabledGateKeyGetter = Substitute.For<IEnabledGateKeyGetter>();

            _ruleFactory = new RuleFactory(_enabledGateKeyGetter);
        }

        [Test]
        public void GetInstance_ReturnsInstanceRuleWithValidParams()
        {
            object instance = new();
            InstanceRule<object> expectedResult = new(instance);

            IRule<object> result = _ruleFactory.GetInstance(instance);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetTransient_ReturnsTransientRuleWithValidParams()
        {
            Func<IRuleResolver, object> ctor = Substitute.For<Func<IRuleResolver, object>>();
            TransientRule<object> expectedResult = new(ctor);

            IRule<object> result = _ruleFactory.GetTransient(ctor);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetSingleton_ReturnsSingletonRuleWithValidParams()
        {
            Func<IRuleResolver, object> ctor = Substitute.For<Func<IRuleResolver, object>>();
            SingletonRule<object> expectedResult = new(ctor);

            IRule<object> result = _ruleFactory.GetSingleton(ctor);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetTo_ReturnsToRuleWithValidParams()
        {
            object key = new();
            ToRule<object, string> expectedResult = new(key);

            IRule<object> result = _ruleFactory.GetTo<object, string>(key);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetGateKey_ReturnsGateKeyRuleWithValidParams()
        {
            IRule<object> rule = Substitute.For<IRule<object>>();
            object gateKey = new();
            GateKeyRule<object> expectedResult = new(_enabledGateKeyGetter, rule, gateKey);

            IRule<object> result = _ruleFactory.GetGateKey(rule, gateKey);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetTarget_ReturnsTargetRuleWithValidParams()
        {
            IRuleResolver ruleResolver = Substitute.For<IRuleResolver>();
            object key = new();
            TargetRule<object> expectedResult = new(ruleResolver, key);

            IRule<object> result = _ruleFactory.GetTarget<object>(ruleResolver, key);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetInject_ReturnsInjectRuleWithValidParams()
        {
            Action<IRuleResolver, object> inject = Substitute.For<Action<IRuleResolver, object>>();
            InjectRule<object> expectedResult = new(inject);

            IRule<Action<object>> result = _ruleFactory.GetInject(inject);

            Assert.AreEqual(expectedResult, result);
        }
    }
}