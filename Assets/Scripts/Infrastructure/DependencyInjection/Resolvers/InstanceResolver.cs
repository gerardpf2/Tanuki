namespace Infrastructure.DependencyInjection.Resolvers
{
    public class InstanceResolver<T> : IResolver<T>
    {
        private readonly T _instance;

        public InstanceResolver(T instance)
        {
            _instance = instance;
        }

        public T Resolve(IScopeResolver _)
        {
            return _instance;
        }
    }
}