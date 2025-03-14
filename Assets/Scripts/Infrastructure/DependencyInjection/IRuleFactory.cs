using System;
using Infrastructure.DependencyInjection.Rules;

namespace Infrastructure.DependencyInjection
{
    public interface IRuleFactory
    {
        IRule<T> GetInstance<T>(T instance);

        IRule<T> GetTransient<T>(Func<IRuleResolver, T> ctor);

        IRule<T> GetSingleton<T>(Func<IRuleResolver, T> ctor);

        IRule<TInput> GetTo<TInput, TOutput>(object key = null) where TOutput : TInput;

        IRule<T> GetGateKey<T>(IRule<T> rule, string gateKey) where T : class;

        IRule<T> GetTarget<T>(IRuleResolver ruleResolver, object key = null);

        IRule<Action<T>> GetInject<T>(Action<IRuleResolver, T> inject);
    }
}