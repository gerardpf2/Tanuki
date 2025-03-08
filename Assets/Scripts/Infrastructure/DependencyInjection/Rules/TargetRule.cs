using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Rules
{
    // TODO: Test
    public class TargetRule<T> : IRule<T>
    {
        private readonly IRuleResolver _ruleResolver;
        private readonly object _key;

        public TargetRule([NotNull] IRuleResolver ruleResolver, object key = null)
        {
            _ruleResolver = ruleResolver;
            _key = key;
        }

        public T Resolve(IRuleResolver _)
        {
            return _ruleResolver.Resolve<T>(_key);
        }
    }
}