using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Rules
{
    // TODO: Test
    // TODO: Equals and GetHashCode
    // To resolve InjectRule<T>, Action<T> needs to be used instead of T
    public class InjectRule<T> : SingletonRule<Action<T>>
    {
        public InjectRule([NotNull] Action<IRuleResolver, T> inject) : base(ruleResolver => instance => inject(ruleResolver, instance)) { }
    }
}