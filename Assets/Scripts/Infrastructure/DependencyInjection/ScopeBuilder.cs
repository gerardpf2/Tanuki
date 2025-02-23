using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeBuilder : IScopeBuildingContext, IScopeBuilder
    {
        private Action<IRuleContainer> _addRules;
        private Action<IRuleResolver> _initialize;
        private Action<ICollection<IScopeComposer>> _addChildScopeComposers;

        public void SetAddRules(Action<IRuleContainer> addRules)
        {
            _addRules = addRules;
        }

        public void SetInitialize(Action<IRuleResolver> initialize)
        {
            _initialize = initialize;
        }

        public void SetAddChildScopeComposers(Action<ICollection<IScopeComposer>> addChildScopeComposers)
        {
            _addChildScopeComposers = addChildScopeComposers;
        }

        public Scope Build([NotNull] IScopeComposer scopeComposer, Scope parentScope)
        {
            Clear();

            scopeComposer.Compose(this);

            IRuleContainer ruleContainer = new RuleContainer();
            IRuleResolver ruleResolver = new RuleResolver(ruleContainer, parentScope?.RuleResolver);
            ICollection<IScopeComposer> childScopeComposers = new List<IScopeComposer>();
            Scope scope = new(scopeComposer, ruleContainer, ruleResolver);

            _addRules?.Invoke(ruleContainer);
            _initialize?.Invoke(ruleResolver);
            _addChildScopeComposers?.Invoke(childScopeComposers);

            foreach (IScopeComposer childScopeComposer in childScopeComposers)
            {
                Scope childScope = Build(childScopeComposer, scope);
                scope.AddChild(childScope);
            }

            return scope;
        }

        private void Clear()
        {
            _addRules = null;
            _initialize = null;
            _addChildScopeComposers = null;
        }
    }
}