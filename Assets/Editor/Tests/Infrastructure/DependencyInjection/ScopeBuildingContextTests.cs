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
            Func<object> getGateKey = Substitute.For<Func<object>>();
            _scopeBuildingContext.GetGateKey = getGateKey;

            Assert.AreSame(getGateKey, _scopeBuildingContext.GetGateKey);
        }

        [Test]
        public void AddRules_NotSet_ReturnsNull()
        {
            Assert.IsNull(_scopeBuildingContext.AddRules);
        }

        [Test]
        public void AddRules_Set_ReturnsValue()
        {
            Action<IRuleAdder, IRuleFactory> addRules = Substitute.For<Action<IRuleAdder, IRuleFactory>>();
            _scopeBuildingContext.AddRules = addRules;

            Assert.AreSame(addRules, _scopeBuildingContext.AddRules);
        }

        [Test]
        public void AddSharedRules_NotSet_ReturnsNull()
        {
            Assert.IsNull(_scopeBuildingContext.AddSharedRules);
        }

        [Test]
        public void AddSharedRules_Set_ReturnsValue()
        {
            Action<IRuleAdder, IRuleFactory> addSharedRules = Substitute.For<Action<IRuleAdder, IRuleFactory>>();
            _scopeBuildingContext.AddSharedRules = addSharedRules;

            Assert.AreSame(addSharedRules, _scopeBuildingContext.AddSharedRules);
        }

        [Test]
        public void GetPartialScopeComposers_NotSet_ReturnsNull()
        {
            Assert.IsNull(_scopeBuildingContext.GetPartialScopeComposers);
        }

        [Test]
        public void GetPartialScopeComposers_Set_ReturnsValue()
        {
            Func<IEnumerable<IScopeComposer>> getPartialScopeComposers = Substitute.For<Func<IEnumerable<IScopeComposer>>>();
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
            Func<IEnumerable<IScopeComposer>> getChildScopeComposers = Substitute.For<Func<IEnumerable<IScopeComposer>>>();
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