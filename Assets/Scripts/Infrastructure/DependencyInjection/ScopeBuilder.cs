using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeBuilder : IScopeBuildingContext, IScopeBuilder
    {
        private readonly IScopeConstructor _scopeConstructor;

        private Action<IRuleContainer> _addRules;
        private Action<IRuleResolver> _initialize;
        private Func<IEnumerable<IScopeComposer>> _getChildScopeComposers;

        public ScopeBuilder([NotNull] IScopeConstructor scopeConstructor)
        {
            _scopeConstructor = scopeConstructor;
        }

        public void SetAddRules(Action<IRuleContainer> addRules)
        {
            _addRules = addRules;
        }

        public void SetInitialize(Action<IRuleResolver> initialize)
        {
            _initialize = initialize;
        }

        public void SetGetChildScopeComposers(Func<IEnumerable<IScopeComposer>> getChildScopeComposers)
        {
            _getChildScopeComposers = getChildScopeComposers;
        }

        public Scope Build([NotNull] IScopeComposer scopeComposer, Scope parentScope)
        {
            Clear();
            Compose(scopeComposer);

            Scope scope = _scopeConstructor.Construct(scopeComposer, parentScope);

            if (scope == null)
            {
                throw new InvalidOperationException(); // TODO
            }

            AddRules(scope.RuleContainer);
            Initialize(scope.RuleResolver);
            BuildChildScopeComposers(scope);

            return scope;
        }

        private void Clear()
        {
            _addRules = null;
            _initialize = null;
            _getChildScopeComposers = null;
        }

        private void Compose([NotNull] IScopeComposer scopeComposer)
        {
            scopeComposer.Compose(this);
        }

        private void AddRules(IRuleContainer ruleContainer)
        {
            _addRules?.Invoke(ruleContainer);
        }

        private void Initialize(IRuleResolver ruleResolver)
        {
            _initialize?.Invoke(ruleResolver);
        }

        private void BuildChildScopeComposers([NotNull] Scope scope)
        {
            IEnumerable<IScopeComposer> childScopeComposers = _getChildScopeComposers?.Invoke();

            if (childScopeComposers == null)
            {
                return;
            }

            foreach (IScopeComposer childScopeComposer in childScopeComposers)
            {
                Scope childScope = Build(childScopeComposer, scope);
                scope.AddChild(childScope);
            }
        }
    }
}