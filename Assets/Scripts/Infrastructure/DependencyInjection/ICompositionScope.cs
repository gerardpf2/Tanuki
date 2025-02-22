namespace Infrastructure.DependencyInjection
{
    public interface ICompositionScope
    {
        T Resolve<T>();

        bool TryResolve<T>(out T service);
    }
}