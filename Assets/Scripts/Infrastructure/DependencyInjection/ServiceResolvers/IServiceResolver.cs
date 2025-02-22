namespace Infrastructure.DependencyInjection.ServiceResolvers
{
    public interface IServiceResolver<out T>
    {
        T Resolve(ICompositionScope compositionScope);
    }
}