using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Rules
{
    public class TransientRule<T> : IRule<T>
    {
        private readonly Func<IRuleResolver, T> _ctor;

        public TransientRule([NotNull] Func<IRuleResolver, T> ctor)
        {
            _ctor = ctor;
        }

        public T Resolve(IRuleResolver ruleResolver)
        {
            return _ctor(ruleResolver);
        }
    }
}