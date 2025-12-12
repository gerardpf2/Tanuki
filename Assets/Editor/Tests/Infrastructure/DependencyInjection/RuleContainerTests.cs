using System;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class RuleContainerTests
    {
        private RuleContainer _ruleContainer;

        [SetUp]
        public void SetUp()
        {
            _ruleContainer = new RuleContainer();
        }

        [Test]
        public void Add_RuleTypeDuplicatedSameKey_ThrowsException()
        {
            IRule<object> rule1 = Substitute.For<IRule<object>>();
            IRule<object> rule2 = Substitute.For<IRule<object>>();
            object key = new();

            _ruleContainer.Add(rule1, key);

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => _ruleContainer.Add(rule2, key));
            Assert.AreEqual($"Cannot add rule with Type: {typeof(object)} and Key: {key}", invalidOperationException.Message);
        }

        [Test]
        public void Add_RuleTypeDuplicatedDifferentKey_DoesNotThrowException()
        {
            IRule<object> rule1 = Substitute.For<IRule<object>>();
            IRule<object> rule2 = Substitute.For<IRule<object>>();
            object key1 = new();
            object key2 = new();

            _ruleContainer.Add(rule1, key1);

            Assert.DoesNotThrow(() => _ruleContainer.Add(rule2, key2));
        }

        [Test]
        public void TryGet_RuleAddedSameKey_ReturnsTrueAndOutResult()
        {
            IRule<object> expectedResult = Substitute.For<IRule<object>>();
            object key = new();
            _ruleContainer.Add(expectedResult, key);

            bool found = _ruleContainer.TryGet(out IRule<object> result, key);

            Assert.IsTrue(found);
            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void TryGet_RuleAddedDifferentKey_ReturnsFalseAndOutNull()
        {
            IRule<object> rule = Substitute.For<IRule<object>>();
            object key1 = new();
            object key2 = new();
            _ruleContainer.Add(rule, key1);

            bool found = _ruleContainer.TryGet(out IRule<object> result, key2);

            Assert.IsFalse(found);
            Assert.IsNull(result);
        }

        [Test]
        public void TryGet_RuleNotAdded_ReturnsFalseAndOutNull()
        {
            bool found = _ruleContainer.TryGet(out IRule<object> result);

            Assert.IsFalse(found);
            Assert.IsNull(result);
        }
    }
}