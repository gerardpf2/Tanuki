namespace Infrastructure.DependencyInjection.Rules
{
    public class InstanceRule<T> : IRule<T>
    {
        private readonly T _instance;

        public InstanceRule(T instance)
        {
            _instance = instance;
        }

        public T Resolve(IRuleResolver _)
        {
            return _instance;
        }
    }
}