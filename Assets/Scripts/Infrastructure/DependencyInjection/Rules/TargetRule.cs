using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.DependencyInjection.Rules
{
    public class TargetRule<T> : IRule<T> where T : class
    {
        [NotNull] private readonly IRuleResolver _ruleResolver;
        private readonly object _key;

        public TargetRule([NotNull] IRuleResolver ruleResolver, object key = null)
        {
            ArgumentNullException.ThrowIfNull(ruleResolver);

            _ruleResolver = ruleResolver;
            _key = key;
        }

        public T Resolve(IRuleResolver _)
        {
            return _ruleResolver.Resolve<T>(_key);
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

            if (obj is not TargetRule<T> other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_ruleResolver, _key);
        }

        protected bool Equals([NotNull] TargetRule<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return Equals(_ruleResolver, other._ruleResolver) && Equals(_key, other._key);
        }
    }
}