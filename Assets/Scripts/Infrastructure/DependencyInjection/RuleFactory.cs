using System;
using Infrastructure.DependencyInjection.Rules;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public class RuleFactory : IRuleFactory
    {
        private readonly IEnabledGateKeyGetter _enabledGateKeyGetter;

        public RuleFactory([NotNull] IEnabledGateKeyGetter enabledGateKeyGetter)
        {
            _enabledGateKeyGetter = enabledGateKeyGetter;
        }

        public IRule<T> GetInstance<T>(T instance)
        {
            return new InstanceRule<T>(instance);
        }

        public IRule<T> GetTransient<T>([NotNull] Func<IRuleResolver, T> ctor)
        {
            return new TransientRule<T>(ctor);
        }

        public IRule<T> GetSingleton<T>([NotNull] Func<IRuleResolver, T> ctor)
        {
            return new SingletonRule<T>(ctor);
        }

        public IRule<TInput> GetTo<TInput, TOutput>(object key = null) where TOutput : TInput
        {
            return new ToRule<TInput, TOutput>(key);
        }

        public IRule<T> GetGateKey<T>([NotNull] IRule<T> rule, object gateKey) where T : class
        {
            return new GateKeyRule<T>(_enabledGateKeyGetter, rule, gateKey);
        }

        // TODO: Test
        public IRule<T> GetTarget<T>([NotNull] IRuleResolver ruleResolver, object key = null)
        {
            return new TargetRule<T>(ruleResolver, key);
        }

        // TODO: Test
        public IRule<Action<T>> GetInject<T>([NotNull] Action<IRuleResolver, T> inject)
        {
            return new InjectRule<T>(inject);
        }
    }
}