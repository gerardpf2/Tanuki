using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeConstructor : IScopeConstructor
    {
        public Scope ConstructPartialOf(
            [NotNull] Scope scope,
            IScopeComposer scopeComposer,
            Action<IRuleResolver> initialize)
        {
            return new Scope(scopeComposer, scope.RuleAdder, scope.RuleResolver, initialize);
        }

        public Scope ConstructChildOf(Scope scope, IScopeComposer scopeComposer, Action<IRuleResolver> initialize)
        {
            RuleContainer ruleContainer = new();
            IRuleResolver ruleResolver = new RuleResolver(ruleContainer, scope?.RuleResolver);

            return new Scope(scopeComposer, ruleContainer, ruleResolver, initialize);
        }
    }
}