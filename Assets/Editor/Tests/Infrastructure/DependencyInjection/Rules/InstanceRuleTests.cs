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
    }
}