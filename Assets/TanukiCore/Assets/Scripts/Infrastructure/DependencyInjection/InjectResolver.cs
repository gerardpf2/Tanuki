using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.DependencyInjection
{
    public class InjectResolver
    {
        private static IRuleResolver _ruleResolver;

        public InjectResolver([NotNull] IRuleResolver ruleResolver)
        {
            ArgumentNullException.ThrowIfNull(ruleResolver);

            _ruleResolver = ruleResolver;
        }

        public static void Resolve<T>(T instance, object key = null)
        {
            _ruleResolver.Resolve<Action<T>>(key)(instance);
        }
    }
}