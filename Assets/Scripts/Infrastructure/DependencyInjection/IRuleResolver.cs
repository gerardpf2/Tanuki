namespace Infrastructure.DependencyInjection
{
    public interface IRuleResolver
    {
        T Resolve<T>(object key = null);

        bool TryResolve<T>(out T result, object key = null);
    }
}