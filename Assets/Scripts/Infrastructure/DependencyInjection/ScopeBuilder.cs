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

        public Scope Build([NotNull] IScopeComposer scopeComposer, Scope parentScope)
        {
            return BuildChildOf(parentScope, scopeComposer);
        }

        private Scope BuildChildOf(Scope scope, [NotNull] IScopeComposer scopeComposer)
        {
            return
                Build(
                    scopeComposer,
                    initialize => _scopeConstructor.ConstructChildOf(scope, scopeComposer, initialize)
                );
        }

        private Scope BuildPartialOf(Scope scope, [NotNull] IScopeComposer scopeComposer)
        {
            return
                Build(
                    scopeComposer,
                    initialize => _scopeConstructor.ConstructPartialOf(scope, scopeComposer, initialize)
                );
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
                throw new InvalidOperationException(); // TODO
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
                Scope partialScope = BuildPartialOf(scope, partialScopeComposer);

                if (partialScope == null)
                {
                    continue;
                }

                scope.AddPartial(partialScope);
            }
        }

        private void BuildChildScopeComposers([NotNull] Scope scope, IEnumerable<IScopeComposer> childScopeComposers)
        {
            if (childScopeComposers == null)
            {
                return;
            }

            foreach (IScopeComposer childScopeComposer in childScopeComposers)
            {
                Scope childScope = BuildChildOf(scope, childScopeComposer);

                if (childScope == null)
                {
                    continue;
                }

                scope.AddChild(childScope);
            }
        }
    }
}