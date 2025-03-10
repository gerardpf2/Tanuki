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

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is not SingletonRule<T> other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_ctor);
        }

        protected bool Equals([NotNull] SingletonRule<T> other)
        {
            return Equals(_ctor, other._ctor);
        }
    }
}