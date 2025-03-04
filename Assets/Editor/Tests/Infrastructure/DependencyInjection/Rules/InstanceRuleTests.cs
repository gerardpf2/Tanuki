using Infrastructure.DependencyInjection.Rules;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection.Rules
{
    public class InstanceRuleTests
    {
        private object _instance;

        private InstanceRule<object> _instanceRule;

        [SetUp]
        public void SetUp()
        {
            _instance = new object();

            _instanceRule = new InstanceRule<object>(_instance);
        }

        [Test]
        public void Resolve_ReturnsInstance()
        {
            object result = _instanceRule.Resolve(null);

            Assert.AreSame(_instance, result);
        }

        [Test]
        public void Equals_OtherNull_ReturnsFalse()
        {
            const InstanceRule<object> other = null;

            Assert.IsFalse(_instanceRule.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_SameRef_ReturnsTrue()
        {
            InstanceRule<object> other = _instanceRule;

            Assert.IsTrue(_instanceRule.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            object other = new();

            Assert.AreNotEqual(_instanceRule, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            InstanceRule<object> other = new(_instance);

            Assert.AreEqual(_instanceRule, other);
        }

        [Test]
        public void Equals_OtherDifferentParams_ReturnsFalse()
        {
            object otherInstance = new();
            InstanceRule<object> other = new(otherInstance);

            Assert.AreNotEqual(_instanceRule, other);
        }

        [Test]
        public void GetHashCode_SameParams_SameReturnedValue()
        {
            InstanceRule<object> other = new(_instance);

            Assert.AreEqual(_instanceRule.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_DifferentParams_DifferentReturnedValue()
        {
            object otherInstance = new();
            InstanceRule<object> other = new(otherInstance);

            Assert.AreNotEqual(_instanceRule.GetHashCode(), other.GetHashCode());
        }
    }
}