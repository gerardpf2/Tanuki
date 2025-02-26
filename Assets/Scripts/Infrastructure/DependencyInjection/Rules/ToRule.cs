using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Rules
{
    public class ToRule<TInput, TOutput> : IRule<TInput> where TOutput : TInput
    {
        private readonly object _keyResolve;

        public ToRule(object keyResolve = null)
        {
            _keyResolve = keyResolve;
        }

        public TInput Resolve([NotNull] IRuleResolver ruleResolver)
        {
            return ruleResolver.Resolve<TOutput>(_keyResolve);
        }
    }
}