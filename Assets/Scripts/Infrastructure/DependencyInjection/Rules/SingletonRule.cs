using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Rules
{
    public class SingletonRule<T> : IRule<T>
    {
        private readonly Func<IRuleResolver, T> _ctor;

        private bool _resolved;
        private T _instance;

        public SingletonRule([NotNull] Func<IRuleResolver, T> ctor)
        {
            _ctor = ctor;
        }

        public T Resolve(IRuleResolver ruleResolver)
        {
            if (_resolved)
            {
                return _instance;
            }

            _instance = _ctor(ruleResolver);
            _resolved = true;

            return _instance;
        }
    }
}