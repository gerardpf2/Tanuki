using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.DependencyInjection.Utils
{
    public static class RuleResolverUtils
    {
        public static void SafeResolve<T>([NotNull] this IRuleResolver ruleResolver, object key = null)
        {
            ArgumentNullException.ThrowIfNull(ruleResolver);

            ruleResolver.TryResolve<T>(out _, key);
        }

        public static void SafeExecute<T>(
            [NotNull] this IRuleResolver ruleResolver,
            [NotNull] Action<T> action,
            object key = null)
        {
            ArgumentNullException.ThrowIfNull(ruleResolver);
            ArgumentNullException.ThrowIfNull(action);

            if (ruleResolver.TryResolve(out T result, key))
            {
                action(result);
            }
        }
    }
}