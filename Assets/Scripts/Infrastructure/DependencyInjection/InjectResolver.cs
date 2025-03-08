using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    // TODO: Test
    public class InjectResolver
    {
        public static InjectResolver Instance { get; private set; }

        private readonly IRuleResolver _ruleResolver;

        public InjectResolver([NotNull] IRuleResolver ruleResolver)
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("Cannot be constructed more than once");
            }

            _ruleResolver = ruleResolver;

            Instance = this;
        }

        public void Resolve<T>(T instance, object key = null)
        {
            _ruleResolver.Resolve<Action<T>>(key)(instance);
        }
    }
}