using Infrastructure.DependencyInjection;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class EnabledGateKeyContainerTests
    {
        private EnabledGateKeyContainer _enabledGateKeyContainer;

        [SetUp]
        public void SetUp()
        {
            _enabledGateKeyContainer = new EnabledGateKeyContainer();
        }

        [Test]
        public void Contains_GateKeyAdded_ReturnsTrue()
        {
            const string gateKey = nameof(gateKey);
            _enabledGateKeyContainer.Add(gateKey);

            bool result = _enabledGateKeyContainer.Contains(gateKey);

            Assert.IsTrue(result);
        }

        [Test]
        public void Contains_GateKeyNotAdded_ReturnsFalse()
        {
            const string gateKey = nameof(gateKey);

            bool result = _enabledGateKeyContainer.Contains(gateKey);

            Assert.IsFalse(result);
        }

        [Test]
        public void Contains_GateKeyNull_ReturnsTrue()
        {
            bool result = _enabledGateKeyContainer.Contains(null);

            Assert.IsTrue(result);
        }
    }
}