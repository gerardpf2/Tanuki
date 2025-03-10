using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class InjectResolver
    {
        private static IRuleResolver _ruleResolver;

        public InjectResolver([NotNull] IRuleResolver ruleResolver)
        {
            _ruleResolver = ruleResolver;
        }

        public static void Resolve<T>(T instance, object key = null)
        {
            _ruleResolver.Resolve<Action<T>>(key)(instance);
        }
    }
}