using System;
using Infrastructure.DependencyInjection.Rules;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class RuleFactory : IRuleFactory
    {
        public InstanceRule<T> GetInstance<T>(T instance)
        {
            return new InstanceRule<T>(instance);
        }

        public TransientRule<T> GetTransient<T>([NotNull] Func<IRuleResolver, T> ctor)
        {
            return new TransientRule<T>(ctor);
        }

        public SingletonRule<T> GetSingleton<T>([NotNull] Func<IRuleResolver, T> ctor)
        {
            return new SingletonRule<T>(ctor);
        }

        public ToRule<TInput, TOutput> GetTo<TInput, TOutput>(object keyResolve = null) where TOutput : TInput
        {
            return new ToRule<TInput, TOutput>(keyResolve);
        }
    }
}