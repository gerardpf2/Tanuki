namespace Infrastructure.DependencyInjection
{
    public class ScopeConstructor : IScopeConstructor
    {
        public Scope Construct(IScopeComposer scopeComposer, Scope parentScope)
        {
            IRuleContainer ruleContainer = new RuleContainer();
            IRuleResolver ruleResolver = new RuleResolver(ruleContainer, parentScope?.RuleResolver);

            return new Scope(scopeComposer, ruleContainer, ruleResolver);
        }
    }
}