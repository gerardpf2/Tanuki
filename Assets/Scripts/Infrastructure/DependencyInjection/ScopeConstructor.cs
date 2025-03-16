using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class ScopeConstructor : IScopeConstructor
    {
        public PartialScope ConstructPartial([NotNull] Scope mainScope, Action<IRuleResolver> initialize)
        {
            IRuleAdder privateRuleAdder = new RuleContainer();
            PartialScope partialScope = new(
                mainScope,
                privateRuleAdder,
                mainScope.PublicRuleAdder,
                mainScope.RuleResolver,
                initialize
            );

            mainScope.AddPartial(partialScope);

            return partialScope;
        }

        public Scope Construct(Scope parentScope, Action<IRuleResolver> initialize)
        {
            IRuleAdder privateRuleAdder = new RuleContainer();
            RuleContainer publicRuleContainer = new();
            IRuleResolver ruleResolver = new RuleResolver(publicRuleContainer, parentScope?.RuleResolver);
            Scope childScope = new(privateRuleAdder, publicRuleContainer, ruleResolver, initialize);

            parentScope?.AddChild(childScope);

            return childScope;
        }
    }
}