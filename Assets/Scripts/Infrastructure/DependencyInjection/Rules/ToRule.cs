using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Rules
{
    public class ToRule<TInput, TOutput> : IRule<TInput> where TOutput : TInput
    {
        private readonly object _keyToResolve;

        public ToRule(object keyToResolve = null)
        {
            _keyToResolve = keyToResolve;
        }

        public TInput Resolve([NotNull] IRuleResolver ruleResolver)
        {
            return ruleResolver.Resolve<TOutput>(_keyToResolve);
        }
    }
}