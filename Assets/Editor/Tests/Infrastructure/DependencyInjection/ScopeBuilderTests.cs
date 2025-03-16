using System;
using System.Collections.Generic;
using Infrastructure.DependencyInjection;
using Infrastructure.Gating;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class ScopeBuilderTests
    {
        private IScopeConstructor _scopeConstructor;
        private IGlobalRuleAdder _globalRuleAdder;
        private IGateValidator _gateValidator;
        private IScopeComposer _scopeComposer;
        private IRuleFactory _ruleFactory;

        private ScopeBuilder _scopeBuilder;

        [SetUp]
        public void SetUp()
        {
            _scopeConstructor = Substitute.For<IScopeConstructor>();
            _globalRuleAdder = Substitute.For<IGlobalRuleAdder>();
            _gateValidator = Substitute.For<IGateValidator>();
            _scopeComposer = Substitute.For<IScopeComposer>();
            _ruleFactory = Substitute.For<IRuleFactory>();

            _scopeBuilder = new ScopeBuilder(_gateValidator, _scopeConstructor, _globalRuleAdder, _ruleFactory);
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
                            c.AddPublicRules == null &&
                            c.GetPartialScopeComposers == null &&
                            c.GetChildScopeComposers == null &&
                            c.Initialize == null
                    )
                );
        }

        [Test]
        public void Build_GateKeyCalledWithValidParams()
        {
            const string gateKey = nameof(gateKey);
            Func<string> getGateKey = Substitute.For<Func<string>>();
            getGateKey.Invoke().Returns(gateKey);
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.GetGateKey = getGateKey));

            _scopeBuilder.Build(null, _scopeComposer);

            _gateValidator.Received(1).Validate(gateKey);
        }

        [Test]
        public void Build_GateKeyNotEnabled_ReturnsNull()
        {
            _gateValidator.Validate(Arg.Any<string>()).Returns(false);

            Scope childScope = _scopeBuilder.Build(null, _scopeComposer);

            Assert.IsNull(childScope);
        }

        [Test]
        public void Build_GateKeyEnabledAndConstructReturnsNotNull_AddPublicRulesCalledWithValidParams()
        {
            _gateValidator.Validate(Arg.Any<string>()).Returns(true);
            IRuleAdder publicRuleAdder = Substitute.For<IRuleAdder>();
            Scope parentScope = new(null, null, null);
            Scope childScope = new(publicRuleAdder, null, null);
            _scopeConstructor.Construct(parentScope, Arg.Any<Action<IRuleResolver>>()).Returns(childScope);
            Action<IRuleAdder, IRuleFactory> addPublicRules = Substitute.For<Action<IRuleAdder, IRuleFactory>>();
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.AddPublicRules = addPublicRules));

            _scopeBuilder.Build(parentScope, _scopeComposer);

            addPublicRules.Received(1).Invoke(publicRuleAdder, _ruleFactory);
        }

        [Test]
        public void Build_GateKeyEnabledAndConstructReturnsNotNull_AddGlobalRulesCalledWithValidParams()
        {
            _gateValidator.Validate(Arg.Any<string>()).Returns(true);
            IRuleAdder publicRuleAdder = Substitute.For<IRuleAdder>();
            IRuleResolver ruleResolver = Substitute.For<IRuleResolver>();
            Scope parentScope = new(null, null, null);
            Scope childScope = new(publicRuleAdder, ruleResolver, null);
            _scopeConstructor.Construct(parentScope, Arg.Any<Action<IRuleResolver>>()).Returns(childScope);
            Action<IRuleAdder, IRuleFactory> addGlobalRules = Substitute.For<Action<IRuleAdder, IRuleFactory>>();
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.AddGlobalRules = addGlobalRules));

            _scopeBuilder.Build(parentScope, _scopeComposer);

            Received.InOrder(
                () =>
                {
                    _globalRuleAdder.SetTarget(publicRuleAdder, ruleResolver);
                    addGlobalRules.Invoke(_globalRuleAdder, _ruleFactory);
                    _globalRuleAdder.ClearTarget();
                }
            );
        }

        [Test]
        public void Build_GateKeyEnabledAndConstructReturnsNotNull_GetPartialScopeComposersCalled()
        {
            _gateValidator.Validate(Arg.Any<string>()).Returns(true);
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
            _gateValidator.Validate(Arg.Any<string>()).Returns(true);
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
            _gateValidator.Validate(Arg.Any<string>()).Returns(true);
            Scope mainScope = new(null, null, null);
            Action<IRuleResolver> initialize = Substitute.For<Action<IRuleResolver>>();
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.Initialize = initialize));

            _scopeBuilder.BuildPartial(mainScope, _scopeComposer);

            _scopeConstructor.Received(1).ConstructPartial(mainScope, initialize);
        }

        [Test]
        public void BuildPartial_GateKeyEnabledAndConstructReturnsNull_ReturnsNull()
        {
            _gateValidator.Validate(Arg.Any<string>()).Returns(true);
            Scope mainScope = new(null, null, null);
            _scopeConstructor.ConstructPartial(mainScope, Arg.Any<Action<IRuleResolver>>()).Returns((PartialScope)null);

            PartialScope partialScope = _scopeBuilder.BuildPartial(mainScope, _scopeComposer);

            Assert.IsNull(partialScope);
        }

        [Test]
        public void BuildPartial_GateKeyEnabledAndConstructReturnsNotNull_ReturnsScope()
        {
            _gateValidator.Validate(Arg.Any<string>()).Returns(true);
            Scope mainScope = new(null, null, null);
            PartialScope expectedPartialScope = new(mainScope, null, null, null);
            _scopeConstructor.ConstructPartial(mainScope, Arg.Any<Action<IRuleResolver>>()).Returns(expectedPartialScope);

            PartialScope partialScope = _scopeBuilder.BuildPartial(mainScope, _scopeComposer);

            Assert.AreSame(expectedPartialScope, partialScope);
        }

        #endregion

        #region Build

        [Test]
        public void Build_GateKeyEnabled_ConstructCalledWithValidParams()
        {
            _gateValidator.Validate(Arg.Any<string>()).Returns(true);
            Scope parentScope = new(null, null, null);
            Action<IRuleResolver> initialize = Substitute.For<Action<IRuleResolver>>();
            _scopeComposer.Compose(Arg.Do<ScopeBuildingContext>(c => c.Initialize = initialize));

            _scopeBuilder.Build(parentScope, _scopeComposer);

            _scopeConstructor.Received(1).Construct(parentScope, initialize);
        }

        [Test]
        public void Build_GateKeyEnabledAndConstructReturnsNull_ReturnsNull()
        {
            _gateValidator.Validate(Arg.Any<string>()).Returns(true);
            Scope parentScope = new(null, null, null);
            _scopeConstructor.Construct(parentScope, Arg.Any<Action<IRuleResolver>>()).Returns((Scope)null);

            Scope childScope = _scopeBuilder.Build(parentScope, _scopeComposer);

            Assert.IsNull(childScope);
        }

        [Test]
        public void Build_GateKeyEnabledAndConstructReturnsNotNull_ReturnsScope()
        {
            _gateValidator.Validate(Arg.Any<string>()).Returns(true);
            Scope parentScope = new(null, null, null);
            Scope expectedChildScope = new(null, null, null);
            _scopeConstructor.Construct(parentScope, Arg.Any<Action<IRuleResolver>>()).Returns(expectedChildScope);

            Scope childScope = _scopeBuilder.Build(parentScope, _scopeComposer);

            Assert.AreSame(expectedChildScope, childScope);
        }

        #endregion
    }
}