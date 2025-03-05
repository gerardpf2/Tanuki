using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeBuilder : IScopeBuilder
    {
        private readonly IEnabledGateKeyGetter _enabledGateKeyGetter;
        private readonly IScopeConstructor _scopeConstructor;
        private readonly IRuleFactory _ruleFactory;

        public ScopeBuilder(
            [NotNull] IEnabledGateKeyGetter enabledGateKeyGetter,
            [NotNull] IScopeConstructor scopeConstructor,
            IRuleFactory ruleFactory)
        {
            _enabledGateKeyGetter = enabledGateKeyGetter;
            _scopeConstructor = scopeConstructor;
            _ruleFactory = ruleFactory;
        }

        // TODO: Test removed AddChild
        public Scope Build(Scope parentScope, [NotNull] IScopeComposer scopeComposer)
        {
            return
                Build(
                    scopeComposer,
                    initialize => _scopeConstructor.Construct(parentScope, initialize)
                );
        }

        // TODO: Test removed AddPartial
        public PartialScope BuildPartial([NotNull] Scope mainScope, [NotNull] IScopeComposer scopeComposer)
        {
            return
                Build(
                    scopeComposer,
                    initialize => _scopeConstructor.ConstructPartial(mainScope, initialize)
                );
        }

        private T Build<T>([NotNull] IScopeComposer scopeComposer, [NotNull] Func<Action<IRuleResolver>, T> ctor) where T : Scope
        {
            ScopeBuildingContext scopeBuildingContext = new();

            scopeComposer.Compose(scopeBuildingContext);

            if (!_enabledGateKeyGetter.Contains(scopeBuildingContext.GetGateKey?.Invoke()))
            {
                return null;
            }

            T scope = ctor(scopeBuildingContext.Initialize);

            if (scope == null)
            {
                return null;
            }

            scopeBuildingContext.AddRules?.Invoke(scope.RuleAdder, _ruleFactory);

            BuildPartialScopeComposers(scope, scopeBuildingContext.GetPartialScopeComposers?.Invoke());
            BuildChildScopeComposers(scope, scopeBuildingContext.GetChildScopeComposers?.Invoke());

            return scope;
        }

        private void BuildPartialScopeComposers(
            [NotNull] Scope mainScope,
            IEnumerable<IScopeComposer> partialScopeComposers)
        {
            if (partialScopeComposers == null)
            {
                return;
            }

            foreach (IScopeComposer partialScopeComposer in partialScopeComposers)
            {
                BuildPartial(mainScope, partialScopeComposer);
            }
        }

        private void BuildChildScopeComposers(Scope parentScope, IEnumerable<IScopeComposer> childScopeComposers)
        {
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