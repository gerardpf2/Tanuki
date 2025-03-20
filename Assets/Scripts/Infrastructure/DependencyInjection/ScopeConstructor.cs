using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.DependencyInjection
{
    public class ScopeConstructor : IScopeConstructor
    {
        public PartialScope ConstructPartial([NotNull] Scope mainScope, Action<IRuleResolver> initialize)
        {
            ArgumentNullException.ThrowIfNull(mainScope);

            PartialScope partialScope = new(mainScope, mainScope.RuleAdder, mainScope.RuleResolver, initialize);

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