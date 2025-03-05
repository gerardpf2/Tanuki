using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeConstructor : IScopeConstructor
    {
        public Scope Construct(Scope parentScope, Action<IRuleResolver> initialize)
        {
            RuleContainer ruleContainer = new();
            IRuleResolver ruleResolver = new RuleResolver(ruleContainer, parentScope?.RuleResolver);

            return new Scope(ruleContainer, ruleResolver, initialize);
        }

        public Scope ConstructPartial([NotNull] Scope mainScope, Action<IRuleResolver> initialize)
        {
            return new Scope(mainScope.RuleAdder, mainScope.RuleResolver, initialize);
        }
    }
}