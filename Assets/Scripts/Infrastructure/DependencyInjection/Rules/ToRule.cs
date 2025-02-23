using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Rules
{
    public class ToRule<TInput, TOutput> : IRule<TInput> where TOutput : TInput
    {
        public TInput Resolve([NotNull] IRuleResolver ruleResolver)
        {
            return ruleResolver.Resolve<TOutput>();
        }
    }
}