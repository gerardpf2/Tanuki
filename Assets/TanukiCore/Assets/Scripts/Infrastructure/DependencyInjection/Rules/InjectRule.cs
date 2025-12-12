using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.DependencyInjection.Rules
{
    // To resolve InjectRule<T>, Action<T> needs to be used instead of T

    public class InjectRule<T> : SingletonRule<Action<T>>
    {
        [NotNull] private readonly Action<IRuleResolver, T> _inject;

        public InjectRule([NotNull] Action<IRuleResolver, T> inject) : base(ruleResolver => instance => inject(ruleResolver, instance))
        {
            ArgumentNullException.ThrowIfNull(inject);

            _inject = inject;
        }

        public override bool Equals(object obj)
        {
            // It is intended not to call base.Equals

            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is not InjectRule<T> other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            // It is intended not to call base.GetHashCode

            return HashCode.Combine(_inject);
        }

        private bool Equals([NotNull] InjectRule<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return Equals(_inject, other._inject);
        }
    }
}