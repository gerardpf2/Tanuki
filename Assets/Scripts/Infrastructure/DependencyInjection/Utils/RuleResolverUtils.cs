using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Utils
{
    public static class RuleResolverUtils
    {
        public static void SafeResolve<T>([NotNull] this IRuleResolver ruleResolver, object key = null)
        {
            ruleResolver.TryResolve<T>(out _, key);
        }

        public static void SafeExecute<T>(
            [NotNull] this IRuleResolver ruleResolver,
            [NotNull] Action<T> action,
            object key = null)
        {
            if (ruleResolver.TryResolve(out T result, key))
            {
                action(result);
            }
        }
    }
}