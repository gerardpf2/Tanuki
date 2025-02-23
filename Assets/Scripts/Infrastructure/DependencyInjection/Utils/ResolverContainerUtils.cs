using System;
using Infrastructure.DependencyInjection.Resolvers;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Utils
{
    public static class ResolverContainerUtils
    {
        public static void AddInstance<T>(this IResolverContainer resolverContainer, T instance)
        {
            resolverContainer.Add(new InstanceResolver<T>(instance));
        }

        public static void AddTransient<T>(this IResolverContainer resolverContainer, [NotNull] Func<IScopeResolver, T> ctor)
        {
            resolverContainer.Add(new TransientResolver<T>(ctor));
        }

        public static void AddSingleton<T>(this IResolverContainer resolverContainer, [NotNull] Func<IScopeResolver, T> ctor)
        {
            resolverContainer.Add(new SingletonResolver<T>(ctor));
        }

        public static void AddTo<TInput, TOutput>(this IResolverContainer resolverContainer) where TOutput : TInput
        {
            resolverContainer.Add(new ToResolver<TInput, TOutput>());
        }
    }
}