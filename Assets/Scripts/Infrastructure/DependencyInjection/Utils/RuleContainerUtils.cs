using System;
using Infrastructure.DependencyInjection.Rules;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Utils
{
    public static class RuleContainerUtils
    {
        public static void AddInstance<T>([NotNull] this IRuleContainer ruleContainer, T instance, object key = null)
        {
            ruleContainer.Add(new InstanceRule<T>(instance), key);
        }

        public static void AddTransient<T>(
            [NotNull] this IRuleContainer ruleContainer,
            [NotNull] Func<IRuleResolver, T> ctor,
            object key = null)
        {
            ruleContainer.Add(new TransientRule<T>(ctor), key);
        }

        public static void AddSingleton<T>(
            [NotNull] this IRuleContainer ruleContainer,
            [NotNull] Func<IRuleResolver, T> ctor,
            object key = null)
        {
            ruleContainer.Add(new SingletonRule<T>(ctor), key);
        }

        public static void AddTo<TInput, TOutput>(
            [NotNull] this IRuleContainer ruleContainer,
            object key = null,
            object keyResolve = null) where TOutput : TInput
        {
            ruleContainer.Add(new ToRule<TInput, TOutput>(keyResolve), key);
        }
    }
}