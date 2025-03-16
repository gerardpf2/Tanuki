namespace Infrastructure.DependencyInjection
{
    public interface IRuleResolver
    {
        T Resolve<T>(IRuleResolver sourceRuleResolver, object key = null);

        bool TryResolve<T>(IRuleResolver sourceRuleResolver, out T result, object key = null);

        T Resolve<T>(object key = null)
        {
            return Resolve<T>(this, key);
        }

        bool TryResolve<T>(out T result, object key = null)
        {
            return TryResolve(this, out result, key);
        }
    }
}