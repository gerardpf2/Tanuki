using System;
using System.Collections.Generic;
using Infrastructure.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class ScopeBuildingContextTests
    {
        private ScopeBuildingContext _scopeBuildingContext;

        [SetUp]
        public void SetUp()
        {
            _scopeBuildingContext = new ScopeBuildingContext();
        }

        [Test]
        public void GetGateKey_NotSet_ReturnsNull()
        {
            Assert.IsNull(_scopeBuildingContext.GetGateKey);
        }

        [Test]
        public void GetGateKey_Set_ReturnsValue()
        {
            Func<string> getGateKey = Substitute.For<Func<string>>();
            _scopeBuildingContext.GetGateKey = getGateKey;

            Assert.AreSame(getGateKey, _scopeBuildingContext.GetGateKey);
        }

        [Test]
        public void AddPrivateRules_NotSet_ReturnsNull()
        {
            Assert.IsNull(_scopeBuildingContext.AddPrivateRules);
        }

        [Test]
        public void AddPrivateRules_Set_ReturnsValue()
        {
            Action<IRuleAdder, IRuleFactory> addPrivateRules = Substitute.For<Action<IRuleAdder, IRuleFactory>>();
            _scopeBuildingContext.AddPrivateRules = addPrivateRules;

            Assert.AreSame(addPrivateRules, _scopeBuildingContext.AddPrivateRules);
        }

        [Test]
        public void AddPublicRules_NotSet_ReturnsNull()
        {
            Assert.IsNull(_scopeBuildingContext.AddPublicRules);
        }

        [Test]
        public void AddPublicRules_Set_ReturnsValue()
        {
            Action<IRuleAdder, IRuleFactory> addPublicRules = Substitute.For<Action<IRuleAdder, IRuleFactory>>();
            _scopeBuildingContext.AddPublicRules = addPublicRules;

            Assert.AreSame(addPublicRules, _scopeBuildingContext.AddPublicRules);
        }

        [Test]
        public void AddGlobalRules_NotSet_ReturnsNull()
        {
            Assert.IsNull(_scopeBuildingContext.AddGlobalRules);
        }

        [Test]
        public void AddGlobalRules_Set_ReturnsValue()
        {
            Action<IRuleAdder, IRuleFactory> addGlobalRules = Substitute.For<Action<IRuleAdder, IRuleFactory>>();
            _scopeBuildingContext.AddGlobalRules = addGlobalRules;

            Assert.AreSame(addGlobalRules, _scopeBuildingContext.AddGlobalRules);
        }

        [Test]
        public void GetPartialScopeComposers_NotSet_ReturnsNull()
        {
            Assert.IsNull(_scopeBuildingContext.GetPartialScopeComposers);
        }

        [Test]
        public void GetPartialScopeComposers_Set_ReturnsValue()
        {
            Func<IRuleResolver, IEnumerable<IScopeComposer>> getPartialScopeComposers = Substitute.For<Func<IRuleResolver, IEnumerable<IScopeComposer>>>();
            _scopeBuildingContext.GetPartialScopeComposers = getPartialScopeComposers;

            Assert.AreSame(getPartialScopeComposers, _scopeBuildingContext.GetPartialScopeComposers);
        }

        [Test]
        public void GetChildScopeComposers_NotSet_ReturnsNull()
        {
            Assert.IsNull(_scopeBuildingContext.GetChildScopeComposers);
        }

        [Test]
        public void GetChildScopeComposers_Set_ReturnsValue()
        {
            Func<IRuleResolver, IEnumerable<IScopeComposer>> getChildScopeComposers = Substitute.For<Func<IRuleResolver, IEnumerable<IScopeComposer>>>();
            _scopeBuildingContext.GetChildScopeComposers = getChildScopeComposers;

            Assert.AreSame(getChildScopeComposers, _scopeBuildingContext.GetChildScopeComposers);
        }

        [Test]
        public void Initialize_NotSet_ReturnsNull()
        {
            Assert.IsNull(_scopeBuildingContext.Initialize);
        }

        [Test]
        public void Initialize_Set_ReturnsValue()
        {
            Action<IRuleResolver> initialize = Substitute.For<Action<IRuleResolver>>();
            _scopeBuildingContext.Initialize = initialize;

            Assert.AreSame(initialize, _scopeBuildingContext.Initialize);
        }
    }
}