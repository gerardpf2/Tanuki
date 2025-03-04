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

        public ToRule<TInput, TOutput> GetTo<TInput, TOutput>(object keyToResolve = null) where TOutput : TInput
        {
            return new ToRule<TInput, TOutput>(keyToResolve);
        }

        public GateKeyRule<T> GetGateKey<T>([NotNull] IRule<T> rule, object gateKey) where T : class
        {
            return new GateKeyRule<T>(_enabledGateKeyGetter, rule, gateKey);
        }
    }
}