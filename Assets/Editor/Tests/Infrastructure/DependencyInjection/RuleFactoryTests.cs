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
            object keyToResolve = new();
            ToRule<object, string> expectedResult = new(keyToResolve);

            IRule<object> result = _ruleFactory.GetTo<object, string>(keyToResolve);

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
    }
}