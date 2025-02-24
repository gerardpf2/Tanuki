using System;

namespace Infrastructure.DependencyInjection
{
    public class ScopeConstructor : IScopeConstructor
    {
        public Scope Construct(IScopeComposer scopeComposer, Scope parentScope, Action<IRuleResolver> initialize)
        {
            IRuleContainer ruleContainer = new RuleContainer();
            IRuleResolver ruleResolver = new RuleResolver(ruleContainer, parentScope?.RuleResolver);

            return Construct(scopeComposer, ruleContainer, ruleResolver, initialize);
        }

        public Scope Construct(
            IScopeComposer scopeComposer,
            IRuleContainer ruleContainer,
            IRuleResolver ruleResolver,
            Action<IRuleResolver> initialize)
        {
            return new Scope(scopeComposer, ruleContainer, ruleResolver, initialize);
        }
    }
}