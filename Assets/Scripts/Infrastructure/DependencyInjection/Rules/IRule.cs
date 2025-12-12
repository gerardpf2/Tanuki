namespace Infrastructure.DependencyInjection.Rules
{
    public interface IRule<out T>
    {
        T Resolve(IRuleResolver ruleResolver);
    }
}