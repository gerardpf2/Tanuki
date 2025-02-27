using System;
using System.Collections.Generic;

namespace Infrastructure.DependencyInjection
{
    public class Scope
    {
        public IReadOnlyCollection<Scope> PartialScopes => _partialScopes;

        public IReadOnlyCollection<Scope> ChildScopes => _childScopes;

        public readonly IScopeComposer ScopeComposer; // TODO: Check if needed
        public readonly IRuleContainer RuleContainer;
        public readonly IRuleResolver RuleResolver;
        public readonly Action<IRuleResolver> Initialize;

        private readonly List<Scope> _partialScopes = new();
        private readonly List<Scope> _childScopes = new();

        public Scope(
            IScopeComposer scopeComposer,
            IRuleContainer ruleContainer,
            IRuleResolver ruleResolver,
            Action<IRuleResolver> initialize)
        {
            ScopeComposer = scopeComposer;
            RuleContainer = ruleContainer;
            RuleResolver = ruleResolver;
            Initialize = initialize;
        }

        public void AddPartial(Scope partialScope)
        {
            _partialScopes.Add(partialScope);
        }

        public void AddChild(Scope childScope)
        {
            _childScopes.Add(childScope);
        }
    }
}