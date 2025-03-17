using System.Collections.Generic;
using Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection.Rules;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.DependencyInjection
{
    public class ScopeComposerTests
    {
        private ScopeBuildingContext _scopeBuildingContext;

        private ScopeComposer _scopeComposer;

        [SetUp]
        public void SetUp()
        {
            _scopeBuildingContext = new ScopeBuildingContext();

            _scopeComposer = new ScopeComposer();
        }

        [Test]
        public void Compose_GetGateKeyReturnsNull()
        {
            _scopeComposer.Compose(_scopeBuildingContext);
            string gateKey = _scopeBuildingContext.GetGateKey();

            Assert.IsNull(gateKey);
        }

        [Test]
        public void Compose_AddRulesDoesNothing()
        {
            IRuleAdder ruleAdder = Substitute.For<IRuleAdder>();
            IRuleFactory ruleFactory = Substitute.For<IRuleFactory>();

            _scopeComposer.Compose(_scopeBuildingContext);
            _scopeBuildingContext.AddRules(ruleAdder, ruleFactory);

            ruleAdder.DidNotReceive().Add(Arg.Any<IRule<object>>(), Arg.Any<object>());
        }

        [Test]
        public void Compose_AddSharedRulesDoesNothing()
        {
            IRuleAdder ruleAdder = Substitute.For<IRuleAdder>();
            IRuleFactory ruleFactory = Substitute.For<IRuleFactory>();

            _scopeComposer.Compose(_scopeBuildingContext);
            _scopeBuildingContext.AddSharedRules(ruleAdder, ruleFactory);

            ruleAdder.DidNotReceive().Add(Arg.Any<IRule<object>>(), Arg.Any<object>());
        }

        [Test]
        public void Compose_GetPartialScopeComposersReturnsEmpty()
        {
            _scopeComposer.Compose(_scopeBuildingContext);
            IEnumerable<IScopeComposer> partialScopeComposers = _scopeBuildingContext.GetPartialScopeComposers();

            Assert.IsEmpty(partialScopeComposers);
        }

        [Test]
        public void Compose_GetChildScopeComposersReturnsEmpty()
        {
            _scopeComposer.Compose(_scopeBuildingContext);
            IEnumerable<IScopeComposer> childScopeComposers = _scopeBuildingContext.GetChildScopeComposers();

            Assert.IsEmpty(childScopeComposers);
        }

        [Test]
        public void Compose_InitializeNotNull()
        {
            _scopeComposer.Compose(_scopeBuildingContext);

            Assert.NotNull(_scopeBuildingContext.Initialize);
        }
    }
}