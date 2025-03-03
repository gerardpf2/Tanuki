using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class ScopeBuilderTests
    {
        private IEnabledGateKeyGetter _enabledGateKeyGetter;
        private IScopeConstructor _scopeConstructor;
        private IScopeComposer _scopeComposer;
        private IRuleFactory _ruleFactory;

        private ScopeBuilder _scopeBuilder;

        [SetUp]
        public void SetUp()
        {
            _enabledGateKeyGetter = Substitute.For<IEnabledGateKeyGetter>();
            _scopeConstructor = Substitute.For<IScopeConstructor>();
            _scopeComposer = Substitute.For<IScopeComposer>();
            _ruleFactory = Substitute.For<IRuleFactory>();

            _scopeBuilder = new ScopeBuilder(_enabledGateKeyGetter, _scopeConstructor, _ruleFactory);
        }

        #region Build (Code shared between BuildAsChildOf and BuildAsPartialOf)

        [Test]
        public void Build_ComposeCalledWithValidParams()
        {
            _scopeBuilder.BuildAsChildOf(null, _scopeComposer);

            _scopeComposer
                .Received(1)
                .Compose(
                    Arg.Is<ScopeBuildingContext>(
                        c =>
                            c != null &&
                            c.GetGateKey == null &&
                            c.AddRules == null &&
                            c.GetPartialScopeComposers == null &&
                            c.GetChildScopeComposers == null &&
                            c.Initialize == null
                    )
                );
        }

        [Test]
        public void Build_GateKeyCalledWithValidParams()
        {
            object gateKey = new();
            Func<object> getGateKey = Substitute.For<Func<object>>();
            getGateKey.Invoke().Returns(gateKey);
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.GetGateKey = getGateKey));

            _scopeBuilder.BuildAsChildOf(null, _scopeComposer);

            _enabledGateKeyGetter.Received(1).Contains(gateKey);
        }

        [Test]
        public void Build_GateKeyNotEnabled_ReturnsNull()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(false);

            Scope scope = _scopeBuilder.BuildAsChildOf(null, _scopeComposer);

            Assert.IsNull(scope);
        }

        [Test]
        public void Build_GateKeyEnabledAndConstructReturnsNotNull_AddRulesCalledWithValidParams()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            IRuleAdder ruleAdder = Substitute.For<IRuleAdder>();
            Scope parentScope = new(null, null, null);
            Scope childScope = new(ruleAdder, null, null);
            _scopeConstructor.ConstructChildOf(parentScope, Arg.Any<Action<IRuleResolver>>()).Returns(childScope);
            Action<IRuleAdder, IRuleFactory> addRules = Substitute.For<Action<IRuleAdder, IRuleFactory>>();
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.AddRules = addRules));

            _scopeBuilder.BuildAsChildOf(parentScope, _scopeComposer);

            addRules.Received(1).Invoke(ruleAdder, _ruleFactory);
        }

        [Test]
        public void Build_GateKeyEnabledAndConstructReturnsNotNull_GetChildScopeComposersCalled()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope parentScope = new(null, null, null);
            Scope childScope = new(null, null, null);
            _scopeConstructor.ConstructChildOf(parentScope, Arg.Any<Action<IRuleResolver>>()).Returns(childScope);
            Func<IEnumerable<IScopeComposer>> getChildScopeComposers = Substitute.For<Func<IEnumerable<IScopeComposer>>>();
            IScopeComposer childScopeComposer = Substitute.For<IScopeComposer>();
            getChildScopeComposers.Invoke().Returns(new List<IScopeComposer> { childScopeComposer });
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.GetChildScopeComposers = getChildScopeComposers));

            _scopeBuilder.BuildAsChildOf(parentScope, _scopeComposer);

            getChildScopeComposers.Received(1).Invoke();
        }

        [Test]
        public void Build_GateKeyEnabledAndConstructReturnsNotNull_GetPartialScopeComposersCalled()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope parentScope = new(null, null, null);
            Scope childScope = new(null, null, null);
            _scopeConstructor.ConstructChildOf(parentScope, Arg.Any<Action<IRuleResolver>>()).Returns(childScope);
            Func<IEnumerable<IScopeComposer>> getPartialScopeComposers = Substitute.For<Func<IEnumerable<IScopeComposer>>>();
            IScopeComposer partialScopeComposer = Substitute.For<IScopeComposer>();
            getPartialScopeComposers.Invoke().Returns(new List<IScopeComposer> { partialScopeComposer });
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.GetPartialScopeComposers = getPartialScopeComposers));

            _scopeBuilder.BuildAsChildOf(parentScope, _scopeComposer);

            getPartialScopeComposers.Received(1).Invoke();
        }

        #endregion

        #region BuildAsChildOf

        [Test]
        public void BuildAsChildOf_GateKeyEnabled_ConstructCalledWithValidParams()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope parentScope = new(null, null, null);
            Action<IRuleResolver> initialize = Substitute.For<Action<IRuleResolver>>();
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.Initialize = initialize));

            _scopeBuilder.BuildAsChildOf(parentScope, _scopeComposer);

            _scopeConstructor.Received(1).ConstructChildOf(parentScope, initialize);
        }

        [Test]
        public void BuildAsChildOf_GateKeyEnabledAndConstructReturnsNull_ReturnsNullAndChildNotAdded()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope parentScope = new(null, null, null);
            _scopeConstructor.ConstructChildOf(parentScope, Arg.Any<Action<IRuleResolver>>()).Returns((Scope)null);

            Scope scope = _scopeBuilder.BuildAsChildOf(parentScope, _scopeComposer);

            Assert.IsNull(scope);
            Assert.IsEmpty(parentScope.ChildScopes);
        }

        [Test]
        public void BuildAsChildOf_GateKeyEnabledAndConstructReturnsNotNull_ReturnsScopeAndChildAdded()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope parentScope = new(null, null, null);
            Scope childScope = new(null, null, null);
            _scopeConstructor.ConstructChildOf(parentScope, Arg.Any<Action<IRuleResolver>>()).Returns(childScope);

            Scope scope = _scopeBuilder.BuildAsChildOf(parentScope, _scopeComposer);

            Assert.AreSame(childScope, scope);
            Assert.IsTrue(parentScope.ChildScopes.Count == 1);
            Assert.AreSame(childScope, parentScope.ChildScopes.First());
        }

        #endregion

        #region BuildAsPartialOf

        [Test]
        public void BuildAsPartialOf_GateKeyEnabled_ConstructCalledWithValidParams()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope partialOfScope = new(null, null, null);
            Action<IRuleResolver> initialize = Substitute.For<Action<IRuleResolver>>();
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.Initialize = initialize));

            _scopeBuilder.BuildAsPartialOf(partialOfScope, _scopeComposer);

            _scopeConstructor.Received(1).ConstructPartialOf(partialOfScope, initialize);
        }

        [Test]
        public void BuildAsPartialOf_GateKeyEnabledAndConstructReturnsNull_ReturnsNullAndPartialNotAdded()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope partialOfScope = new(null, null, null);
            _scopeConstructor.ConstructPartialOf(partialOfScope, Arg.Any<Action<IRuleResolver>>()).Returns((Scope)null);

            Scope scope = _scopeBuilder.BuildAsPartialOf(partialOfScope, _scopeComposer);

            Assert.IsNull(scope);
            Assert.IsEmpty(partialOfScope.PartialScopes);
        }

        [Test]
        public void BuildAsPartialOf_GateKeyEnabledAndConstructReturnsNotNull_ReturnsScopeAndPartialAdded()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope partialOfScope = new(null, null, null);
            Scope partialScope = new(null, null, null);
            _scopeConstructor.ConstructPartialOf(partialOfScope, Arg.Any<Action<IRuleResolver>>()).Returns(partialScope);

            Scope scope = _scopeBuilder.BuildAsPartialOf(partialOfScope, _scopeComposer);

            Assert.AreSame(partialScope, scope);
            Assert.IsTrue(partialOfScope.PartialScopes.Count == 1);
            Assert.AreSame(partialScope, partialOfScope.PartialScopes.First());
        }

        #endregion
    }
}