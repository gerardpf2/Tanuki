using Infrastructure.DependencyInjection.Resolvers;

namespace Infrastructure.DependencyInjection
{
    public interface IResolverContainer
    {
        void Add<T>(IResolver<T> resolver);

        bool TryGet<T>(out IResolver<T> resolver);
    }
}