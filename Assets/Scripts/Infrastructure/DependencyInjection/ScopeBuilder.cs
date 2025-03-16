using System;
using System.Collections.Generic;
using Infrastructure.Gating;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeBuilder : IScopeBuilder
    {
        private readonly IGateValidator _gateValidator;
        private readonly IScopeConstructor _scopeConstructor;
        private readonly IGlobalRuleAdder _globalRuleAdder;
        private readonly IRuleFactory _ruleFactory;

        public ScopeBuilder(
            [NotNull] IGateValidator gateValidator,
            [NotNull] IScopeConstructor scopeConstructor,
            [NotNull] IGlobalRuleAdder globalRuleAdder,
            IRuleFactory ruleFactory)
        {
            _gateValidator = gateValidator;
            _scopeConstructor = scopeConstructor;
            _globalRuleAdder = globalRuleAdder;
            _ruleFactory = ruleFactory;
        }

        public PartialScope BuildPartial(Scope mainScope, [NotNull] IScopeComposer scopeComposer)
        {
            return
                Build(
                    scopeComposer,
                    initialize => _scopeConstructor.ConstructPartial(mainScope, initialize)
                );
        }

        public Scope Build(Scope parentScope, [NotNull] IScopeComposer scopeComposer)
        {
            return
                Build(
                    scopeComposer,
                    initialize => _scopeConstructor.Construct(parentScope, initialize)
                );
        }

        private T Build<T>([NotNull] IScopeComposer scopeComposer, [NotNull] Func<Action<IRuleResolver>, T> ctor) where T : Scope
        {
            ScopeBuildingContext scopeBuildingContext = new();

            scopeComposer.Compose(scopeBuildingContext);

            if (!_gateValidator.Validate(scopeBuildingContext.GetGateKey?.Invoke()))
            {
                return null;
            }

            T scope = ctor(scopeBuildingContext.Initialize);

            if (scope == null)
            {
                return null;
            }

            AddPrivateRules(scope, scopeBuildingContext.AddPrivateRules);
            AddPublicRules(scope, scopeBuildingContext.AddPublicRules);
            AddGlobalRules(scope, scopeBuildingContext.AddGlobalRules);
            BuildPartialScopeComposers(scope, scopeBuildingContext.GetPartialScopeComposers);
            BuildChildScopeComposers(scope, scopeBuildingContext.GetChildScopeComposers);

            return scope;
        }

        private void AddPrivateRules([NotNull] Scope scope, Action<IRuleAdder, IRuleFactory> addPrivateRules)
        {
            addPrivateRules?.Invoke(scope.PrivateRuleAdder, _ruleFactory);
        }

        private void AddPublicRules([NotNull] Scope scope, Action<IRuleAdder, IRuleFactory> addPublicRules)
        {
            addPublicRules?.Invoke(scope.PublicRuleAdder, _ruleFactory);
        }

        private void AddGlobalRules([NotNull] Scope scope, Action<IRuleAdder, IRuleFactory> addGlobalRules)
        {
            _globalRuleAdder.SetTarget(scope.PrivateRuleAdder, scope.RuleResolver);

            addGlobalRules?.Invoke(_globalRuleAdder, _ruleFactory);

            _globalRuleAdder.ClearTarget();
        }

        private void BuildPartialScopeComposers(
            Scope mainScope,
            Func<IRuleResolver, IEnumerable<IScopeComposer>> getPartialScopeComposers)
        {
            IEnumerable<IScopeComposer> partialScopeComposers = getPartialScopeComposers?.Invoke(mainScope.RuleResolver);

            if (partialScopeComposers == null)
            {
                return;
            }

            foreach (IScopeComposer partialScopeComposer in partialScopeComposers)
            {
                BuildPartial(mainScope, partialScopeComposer);
            }
        }

        private void BuildChildScopeComposers(
            Scope parentScope,
            Func<IRuleResolver, IEnumerable<IScopeComposer>> getChildScopeComposers)
        {
            IEnumerable<IScopeComposer> childScopeComposers = getChildScopeComposers?.Invoke(parentScope.RuleResolver);

            if (childScopeComposers == null)
            {
                return;
            }

            foreach (IScopeComposer childScopeComposer in childScopeComposers)
            {
                Build(parentScope, childScopeComposer);
            }
        }
    }
}