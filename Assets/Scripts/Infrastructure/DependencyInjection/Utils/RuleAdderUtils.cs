using System;
using Infrastructure.DependencyInjection.Rules;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Utils
{
    public static class RuleAdderUtils
    {
        public static void AddInstance<T>([NotNull] this IRuleAdder ruleAdder, T instance, object key = null)
        {
            ruleAdder.Add(new InstanceRule<T>(instance), key);
        }

        public static void AddTransient<T>(
            [NotNull] this IRuleAdder ruleAdder,
            [NotNull] Func<IRuleResolver, T> ctor,
            object key = null)
        {
            ruleAdder.Add(new TransientRule<T>(ctor), key);
        }

        public static void AddSingleton<T>(
            [NotNull] this IRuleAdder ruleAdder,
            [NotNull] Func<IRuleResolver, T> ctor,
            object key = null)
        {
            ruleAdder.Add(new SingletonRule<T>(ctor), key);
        }

        public static void AddTo<TInput, TOutput>(
            [NotNull] this IRuleAdder ruleAdder,
            object key = null,
            object keyResolve = null) where TOutput : TInput
        {
            ruleAdder.Add(new ToRule<TInput, TOutput>(keyResolve), key);
        }
    }
}