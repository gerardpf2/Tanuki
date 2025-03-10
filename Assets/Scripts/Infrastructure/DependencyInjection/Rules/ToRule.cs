using System;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Rules
{
    public class ToRule<TInput, TOutput> : IRule<TInput> where TOutput : TInput
    {
        private readonly object _key;

        public ToRule(object key = null)
        {
            _key = key;
        }

        public TInput Resolve([NotNull] IRuleResolver ruleResolver)
        {
            return ruleResolver.Resolve<TOutput>(_key);
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
            return HashCode.Combine(_key);
        }

        protected bool Equals([NotNull] ToRule<TInput, TOutput> other)
        {
            return Equals(_key, other._key);
        }
    }
}