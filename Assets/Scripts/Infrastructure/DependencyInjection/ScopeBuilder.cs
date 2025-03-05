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

        public Scope Build(Scope parentScope, [NotNull] IScopeComposer scopeComposer)
        {
            Scope childScope = Build(
                scopeComposer,
                initialize => _scopeConstructor.Construct(parentScope, initialize)
            );

            if (parentScope != null && childScope != null)
            {
                parentScope.AddChild(childScope);
            }

            return childScope;
        }

        public Scope BuildPartial([NotNull] Scope mainScope, [NotNull] IScopeComposer scopeComposer)
        {
            Scope partialScope = Build(
                scopeComposer,
                initialize => _scopeConstructor.ConstructPartial(mainScope, initialize)
            );

            if (partialScope != null)
            {
                mainScope.AddPartial(partialScope);
            }

            return partialScope;
        }

        private Scope Build([NotNull] IScopeComposer scopeComposer, [NotNull] Func<Action<IRuleResolver>, Scope> ctor)
        {
            ScopeBuildingContext scopeBuildingContext = new();

            scopeComposer.Compose(scopeBuildingContext);

            if (!_enabledGateKeyGetter.Contains(scopeBuildingContext.GetGateKey?.Invoke()))
            {
                return null;
            }

            Scope scope = ctor(scopeBuildingContext.Initialize);

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