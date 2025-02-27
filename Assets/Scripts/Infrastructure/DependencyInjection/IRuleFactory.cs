using System;
using Infrastructure.DependencyInjection.Rules;

namespace Infrastructure.DependencyInjection
{
    public interface IRuleFactory
    {
        InstanceRule<T> GetInstance<T>(T instance);

        TransientRule<T> GetTransient<T>(Func<IRuleResolver, T> ctor);

        SingletonRule<T> GetSingleton<T>(Func<IRuleResolver, T> ctor);

        ToRule<TInput, TOutput> GetTo<TInput, TOutput>(object keyResolve = null) where TOutput : TInput;

        GateKeyRule<T> GetGateKey<T>(IRule<T> rule, object gateKey) where T : class;
    }
}