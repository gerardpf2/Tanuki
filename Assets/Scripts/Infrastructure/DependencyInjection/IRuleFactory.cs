using System;
using Infrastructure.DependencyInjection.Rules;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection
{
    public interface IRuleFactory
    {
        [NotNull]
        IRule<T> GetInstance<T>(T instance);

        [NotNull]
        IRule<T> GetTransient<T>(Func<IRuleResolver, T> ctor);

        [NotNull]
        IRule<T> GetSingleton<T>(Func<IRuleResolver, T> ctor);

        [NotNull]
        IRule<TInput> GetTo<TInput, TOutput>(object key = null) where TOutput : TInput;

        [NotNull]
        IRule<T> GetGateKey<T>(IRule<T> rule, string gateKey) where T : class;

        [NotNull]
        IRule<T> GetTarget<T>(IRuleResolver ruleResolver, object key = null);

        [NotNull]
        IRule<Action<T>> GetInject<T>(Action<IRuleResolver, T> inject);
    }
}