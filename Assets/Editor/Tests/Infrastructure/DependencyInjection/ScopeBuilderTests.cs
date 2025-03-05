using System;
using System.Collections.Generic;
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

        #region Build base (Code shared between Build and BuildPartial)

        [Test]
        public void Build_ComposeCalledWithValidParams()
        {
            _scopeBuilder.Build(null, _scopeComposer);

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

            _scopeBuilder.Build(null, _scopeComposer);

            _enabledGateKeyGetter.Received(1).Contains(gateKey);
        }

        [Test]
        public void Build_GateKeyNotEnabled_ReturnsNull()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(false);

            Scope childScope = _scopeBuilder.Build(null, _scopeComposer);

            Assert.IsNull(childScope);
        }

        [Test]
        public void Build_GateKeyEnabledAndConstructReturnsNotNull_AddRulesCalledWithValidParams()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            IRuleAdder ruleAdder = Substitute.For<IRuleAdder>();
            Scope parentScope = new(null, null, null);
            Scope childScope = new(ruleAdder, null, null);
            _scopeConstructor.Construct(parentScope, Arg.Any<Action<IRuleResolver>>()).Returns(childScope);
            Action<IRuleAdder, IRuleFactory> addRules = Substitute.For<Action<IRuleAdder, IRuleFactory>>();
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.AddRules = addRules));

            _scopeBuilder.Build(parentScope, _scopeComposer);

            addRules.Received(1).Invoke(ruleAdder, _ruleFactory);
        }

        [Test]
        public void Build_GateKeyEnabledAndConstructReturnsNotNull_GetPartialScopeComposersCalled()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope parentScope = new(null, null, null);
            Scope childScope = new(null, null, null);
            _scopeConstructor.Construct(parentScope, Arg.Any<Action<IRuleResolver>>()).Returns(childScope);
            Func<IEnumerable<IScopeComposer>> getPartialScopeComposers = Substitute.For<Func<IEnumerable<IScopeComposer>>>();
            IScopeComposer partialScopeComposer = Substitute.For<IScopeComposer>();
            getPartialScopeComposers.Invoke().Returns(new List<IScopeComposer> { partialScopeComposer });
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.GetPartialScopeComposers = getPartialScopeComposers));

            _scopeBuilder.Build(parentScope, _scopeComposer);

            getPartialScopeComposers.Received(1).Invoke();
        }

        [Test]
        public void Build_GateKeyEnabledAndConstructReturnsNotNull_GetChildScopeComposersCalled()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope parentScope = new(null, null, null);
            Scope childScope = new(null, null, null);
            _scopeConstructor.Construct(parentScope, Arg.Any<Action<IRuleResolver>>()).Returns(childScope);
            Func<IEnumerable<IScopeComposer>> getChildScopeComposers = Substitute.For<Func<IEnumerable<IScopeComposer>>>();
            IScopeComposer childScopeComposer = Substitute.For<IScopeComposer>();
            getChildScopeComposers.Invoke().Returns(new List<IScopeComposer> { childScopeComposer });
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.GetChildScopeComposers = getChildScopeComposers));

            _scopeBuilder.Build(parentScope, _scopeComposer);

            getChildScopeComposers.Received(1).Invoke();
        }

        #endregion

        #region BuildPartial

        [Test]
        public void BuildPartial_GateKeyEnabled_ConstructCalledWithValidParams()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope mainScope = new(null, null, null);
            Action<IRuleResolver> initialize = Substitute.For<Action<IRuleResolver>>();
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.Initialize = initialize));

            _scopeBuilder.BuildPartial(mainScope, _scopeComposer);

            _scopeConstructor.Received(1).ConstructPartial(mainScope, initialize);
        }

        [Test]
        public void BuildPartial_GateKeyEnabledAndConstructReturnsNull_ReturnsNull()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope mainScope = new(null, null, null);
            _scopeConstructor.ConstructPartial(mainScope, Arg.Any<Action<IRuleResolver>>()).Returns((PartialScope)null);

            PartialScope partialScope = _scopeBuilder.BuildPartial(mainScope, _scopeComposer);

            Assert.IsNull(partialScope);
        }

        [Test]
        public void BuildPartial_GateKeyEnabledAndConstructReturnsNotNull_ReturnsScope()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope mainScope = new(null, null, null);
            PartialScope expectedPartialScope = new(mainScope, null);
            _scopeConstructor.ConstructPartial(mainScope, Arg.Any<Action<IRuleResolver>>()).Returns(expectedPartialScope);

            PartialScope partialScope = _scopeBuilder.BuildPartial(mainScope, _scopeComposer);

            Assert.AreSame(expectedPartialScope, partialScope);
        }

        #endregion

        #region Build

        [Test]
        public void Build_GateKeyEnabled_ConstructCalledWithValidParams()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope parentScope = new(null, null, null);
            Action<IRuleResolver> initialize = Substitute.For<Action<IRuleResolver>>();
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.Initialize = initialize));

            _scopeBuilder.Build(parentScope, _scopeComposer);

            _scopeConstructor.Received(1).Construct(parentScope, initialize);
        }

        [Test]
        public void Build_GateKeyEnabledAndConstructReturnsNull_ReturnsNull()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope parentScope = new(null, null, null);
            _scopeConstructor.Construct(parentScope, Arg.Any<Action<IRuleResolver>>()).Returns((Scope)null);

            Scope childScope = _scopeBuilder.Build(parentScope, _scopeComposer);

            Assert.IsNull(childScope);
        }

        [Test]
        public void Build_GateKeyEnabledAndConstructReturnsNotNull_ReturnsScope()
        {
            _enabledGateKeyGetter.Contains(Arg.Any<object>()).Returns(true);
            Scope parentScope = new(null, null, null);
            Scope expectedChildScope = new(null, null, null);
            _scopeConstructor.Construct(parentScope, Arg.Any<Action<IRuleResolver>>()).Returns(expectedChildScope);

            Scope childScope = _scopeBuilder.Build(parentScope, _scopeComposer);

            Assert.AreSame(expectedChildScope, childScope);
        }

        #endregion
    }
}