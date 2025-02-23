using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Resolvers
{
    public class ToResolver<TInput, TOutput> : IResolver<TInput> where TOutput : TInput
    {
        public TInput Resolve([NotNull] IScopeResolver scopeResolver)
        {
            return scopeResolver.Resolve<TOutput>();
        }
    }
}