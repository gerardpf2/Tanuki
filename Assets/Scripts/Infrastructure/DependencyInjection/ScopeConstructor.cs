using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeConstructor : IScopeConstructor
    {
        // TODO: Test added AddChild
        public Scope Construct(Scope parentScope, Action<IRuleResolver> initialize)
        {
            RuleContainer ruleContainer = new();
            IRuleResolver ruleResolver = new RuleResolver(ruleContainer, parentScope?.RuleResolver);
            Scope childScope = new(ruleContainer, ruleResolver, initialize);

            parentScope?.AddChild(childScope);

            return childScope;
        }

        // TODO: Test added AddPartial
        public Scope ConstructPartial([NotNull] Scope mainScope, Action<IRuleResolver> initialize)
        {
            Scope partialScope = new(mainScope.RuleAdder, mainScope.RuleResolver, initialize);

            mainScope.AddPartial(partialScope);

            return partialScope;
        }
    }
}