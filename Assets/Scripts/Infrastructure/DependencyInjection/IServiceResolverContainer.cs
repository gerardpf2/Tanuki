using Infrastructure.DependencyInjection.ServiceResolvers;

namespace Infrastructure.DependencyInjection
{
    public interface IServiceResolverContainer
    {
        void Add<T>(IServiceResolver<T> serviceResolver);

        bool TryGet<T>(out IServiceResolver<T> serviceResolver);
    }
}