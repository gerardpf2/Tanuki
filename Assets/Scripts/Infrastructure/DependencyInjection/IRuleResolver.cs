namespace Infrastructure.DependencyInjection
{
    public interface IRuleResolver
    {
        T Resolve<T>();

        bool TryResolve<T>(out T result);
    }
}