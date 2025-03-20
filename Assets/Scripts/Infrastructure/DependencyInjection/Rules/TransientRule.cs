using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.DependencyInjection.Rules
{
    public class TransientRule<T> : IRule<T>
    {
        [NotNull] private readonly Func<IRuleResolver, T> _ctor;

        public TransientRule([NotNull] Func<IRuleResolver, T> ctor)
        {
            ArgumentNullException.ThrowIfNull(ctor);

            _ctor = ctor;
        }

        public T Resolve(IRuleResolver ruleResolver)
        {
            return _ctor(ruleResolver);
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

            if (obj is not TransientRule<T> other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_ctor);
        }

        protected bool Equals([NotNull] TransientRule<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return Equals(_ctor, other._ctor);
        }
    }
}