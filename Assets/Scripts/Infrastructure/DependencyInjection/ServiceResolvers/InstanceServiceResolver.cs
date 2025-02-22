namespace Infrastructure.DependencyInjection.ServiceResolvers
{
    public class InstanceServiceResolver<T> : IServiceResolver<T>
    {
        private readonly T _instance;

        public InstanceServiceResolver(T instance)
        {
            _instance = instance;
        }

        public T Resolve(ICompositionScope _)
        {
            return _instance;
        }
    }
}