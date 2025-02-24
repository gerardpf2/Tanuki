using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeBuilder : IScopeBuildingContext, IScopeBuilder
    {
        private readonly IScopeConstructor _scopeConstructor;

        private Action<IRuleContainer> _addRules;
        private Func<IEnumerable<IScopeComposer>> _getPartialScopeComposers;
        private Func<IEnumerable<IScopeComposer>> _getChildScopeComposers;
        private Action<IRuleResolver> _initialize;

        public ScopeBuilder([NotNull] IScopeConstructor scopeConstructor)
        {
            _scopeConstructor = scopeConstructor;
        }

        public void SetAddRules(Action<IRuleContainer> addRules)
        {
            _addRules = addRules;
        }

        public void SetGetPartialScopeComposers(Func<IEnumerable<IScopeComposer>> getPartialScopeComposers)
        {
            _getPartialScopeComposers = getPartialScopeComposers;
        }

        public void SetGetChildScopeComposers(Func<IEnumerable<IScopeComposer>> getChildScopeComposers)
        {
            _getChildScopeComposers = getChildScopeComposers;
        }

        public void SetInitialize(Action<IRuleResolver> initialize)
        {
            _initialize = initialize;
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
            Clear();

            scopeComposer.Compose(this);

            Action<IRuleContainer> addRules = _addRules;
            Func<IEnumerable<IScopeComposer>> getPartialScopeComposers = _getPartialScopeComposers;
            Func<IEnumerable<IScopeComposer>> getChildScopeComposers = _getChildScopeComposers;
            Action<IRuleResolver> initialize = _initialize;

            Scope scope = ctor(initialize);

            if (scope == null)
            {
                throw new InvalidOperationException(); // TODO
            }

            addRules?.Invoke(scope.RuleContainer);

            BuildPartialScopeComposers(scope, getPartialScopeComposers?.Invoke());
            BuildChildScopeComposers(scope, getChildScopeComposers?.Invoke());

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

        private void Clear()
        {
            _addRules = null;
            _getPartialScopeComposers = null;
            _getChildScopeComposers = null;
            _initialize = null;
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