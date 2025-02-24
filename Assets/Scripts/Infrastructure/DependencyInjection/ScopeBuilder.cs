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
            return BuildScope(scopeComposer, parentScope);
        }

        private Scope BuildScope([NotNull] IScopeComposer scopeComposer, Scope parentScope)
        {
            return
                Build(
                    scopeComposer,
                    initialize => ConstructScope(scopeComposer, parentScope, initialize)
                );
        }

        private Scope BuildPartialScope([NotNull] IScopeComposer scopeComposer, [NotNull] Scope scope)
        {
            return
                Build(
                    scopeComposer,
                    initialize => ConstructPartialScope(scopeComposer, scope, initialize)
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

        private Scope ConstructScope(IScopeComposer scopeComposer, Scope parentScope, Action<IRuleResolver> initialize)
        {
            return _scopeConstructor.Construct(scopeComposer, parentScope, initialize);
        }

        private Scope ConstructPartialScope(
            IScopeComposer scopeComposer,
            [NotNull] Scope scope,
            Action<IRuleResolver> initialize)
        {
            return
                _scopeConstructor.Construct(
                    scopeComposer,
                    scope.RuleContainer,
                    scope.RuleResolver,
                    initialize
                );
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
                Scope partialScope = BuildPartialScope(partialScopeComposer, scope);
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
                Scope childScope = BuildScope(childScopeComposer, scope);
                scope.AddChild(childScope);
            }
        }
    }
}