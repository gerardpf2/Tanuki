using System.Collections.Generic;

namespace Infrastructure.DependencyInjection
{
    public class Scope
    {
        public IReadOnlyCollection<Scope> ChildScopes => _childScopes;

        public readonly IScopeComposer ScopeComposer;
        public readonly IRuleContainer RuleContainer;
        public readonly IRuleResolver RuleResolver;

        private readonly List<Scope> _childScopes = new();

        public Scope(IScopeComposer scopeComposer, IRuleContainer ruleContainer, IRuleResolver ruleResolver)
        {
            ScopeComposer = scopeComposer;
            RuleContainer = ruleContainer;
            RuleResolver = ruleResolver;
        }

        public void AddChild(Scope childScope)
        {
            _childScopes.Add(childScope);
        }
    }
}