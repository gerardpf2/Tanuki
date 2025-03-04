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

        public Scope BuildAsChildOf(Scope scope, [NotNull] IScopeComposer scopeComposer)
        {
            Scope childScope = Build(
                scopeComposer,
                initialize => _scopeConstructor.ConstructChildOf(scope, initialize)
            );

            if (scope != null && childScope != null)
            {
                scope.AddChild(childScope);
            }

            return childScope;
        }

        public Scope BuildAsPartialOf([NotNull] Scope scope, [NotNull] IScopeComposer scopeComposer)
        {
            Scope partialScope = Build(
                scopeComposer,
                initialize => _scopeConstructor.ConstructPartialOf(scope, initialize)
            );

            if (partialScope != null)
            {
                scope.AddPartial(partialScope);
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
            [NotNull] Scope scope,
            IEnumerable<IScopeComposer> partialScopeComposers)
        {
            if (partialScopeComposers == null)
            {
                return;
            }

            foreach (IScopeComposer partialScopeComposer in partialScopeComposers)
            {
                BuildAsPartialOf(scope, partialScopeComposer);
            }
        }

        private void BuildChildScopeComposers(Scope scope, IEnumerable<IScopeComposer> childScopeComposers)
        {
            if (childScopeComposers == null)
            {
                return;
            }

            foreach (IScopeComposer childScopeComposer in childScopeComposers)
            {
                BuildAsChildOf(scope, childScopeComposer);
            }
        }
    }
}