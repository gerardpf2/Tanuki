using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeConstructor : IScopeConstructor
    {
        public PartialScope ConstructPartial([NotNull] Scope mainScope, Action<IRuleResolver> initialize)
        {
            PartialScope partialScope = new(mainScope, initialize);

            mainScope.AddPartial(partialScope);

            return partialScope;
        }

        public Scope Construct(Scope parentScope, Action<IRuleResolver> initialize)
        {
            RuleContainer ruleContainer = new();
            IRuleResolver ruleResolver = new RuleResolver(ruleContainer, parentScope?.RuleResolver);
            Scope childScope = new(ruleContainer, ruleResolver, initialize);

            parentScope?.AddChild(childScope);

            return childScope;
        }
    }
}