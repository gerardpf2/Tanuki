namespace Infrastructure.DependencyInjection
{
    public interface IGlobalRuleAdder : IRuleAdder
    {
        void SetTarget(IRuleAdder ruleAdder, IRuleResolver ruleResolver);

        void ClearTarget();
    }
}