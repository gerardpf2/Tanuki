namespace Infrastructure.DependencyInjection
{
    public interface IScopeResolver
    {
        T Resolve<T>();

        bool TryResolve<T>(out T result);
    }
}