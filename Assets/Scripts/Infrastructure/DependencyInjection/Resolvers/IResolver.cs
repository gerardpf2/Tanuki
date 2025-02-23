namespace Infrastructure.DependencyInjection.Resolvers
{
    public interface IResolver<out T>
    {
        T Resolve(IScopeResolver scopeResolver);
    }
}