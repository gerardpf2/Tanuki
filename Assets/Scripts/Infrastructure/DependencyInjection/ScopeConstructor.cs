using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeConstructor : IScopeConstructor
    {
        public Scope ConstructPartialOf([NotNull] Scope scope, Action<IRuleResolver> initialize)
        {
            return new Scope(scope.RuleAdder, scope.RuleResolver, initialize);
        }

        public Scope ConstructChildOf(Scope scope, Action<IRuleResolver> initialize)
        {
            RuleContainer ruleContainer = new();
            IRuleResolver ruleResolver = new RuleResolver(ruleContainer, scope?.RuleResolver);

            return new Scope(ruleContainer, ruleResolver, initialize);
        }
    }
}