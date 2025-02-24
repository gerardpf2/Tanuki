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
            return new Scope(scopeComposer, scope.RuleContainer, scope.RuleResolver, initialize);
        }

        public Scope ConstructChildOf(Scope scope, IScopeComposer scopeComposer, Action<IRuleResolver> initialize)
        {
            IRuleContainer ruleContainer = new RuleContainer();
            IRuleResolver ruleResolver = new RuleResolver(ruleContainer, scope?.RuleResolver);

            return new Scope(scopeComposer, ruleContainer, ruleResolver, initialize);
        }
    }
}