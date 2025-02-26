using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeBuilder : IScopeBuilder
    {
        private readonly IScopeConstructor _scopeConstructor;

        public ScopeBuilder([NotNull] IScopeConstructor scopeConstructor)
        {
            _scopeConstructor = scopeConstructor;
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

            Scope scope = ctor(scopeBuildingContext.Initialize);

            if (scope == null)
            {
                throw new InvalidOperationException(); // TODO
            }

            scopeBuildingContext.AddRules?.Invoke(scope.RuleContainer);

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
                scope.AddChild(childScope);
            }
        }
    }
}