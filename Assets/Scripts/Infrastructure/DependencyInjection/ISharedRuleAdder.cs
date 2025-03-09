namespace Infrastructure.DependencyInjection
{
    public interface ISharedRuleAdder : IRuleAdder
    {
        void SetTarget(IRuleAdder ruleAdder, IRuleResolver ruleResolver);

        void ClearTarget();
    }
}