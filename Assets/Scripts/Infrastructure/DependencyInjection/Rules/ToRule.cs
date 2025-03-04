using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Rules
{
    public class ToRule<TInput, TOutput> : IRule<TInput> where TOutput : TInput
    {
        private readonly object _keyToResolve;

        public ToRule(object keyToResolve = null)
        {
            _keyToResolve = keyToResolve;
        }

        public TInput Resolve([NotNull] IRuleResolver ruleResolver)
        {
            return ruleResolver.Resolve<TOutput>(_keyToResolve);
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

            if (obj is not ToRule<TInput, TOutput> other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_keyToResolve);
        }

        protected bool Equals(ToRule<TInput, TOutput> other)
        {
            return Equals(_keyToResolve, other._keyToResolve);
        }
    }
}