using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.DependencyInjection.Rules
{
    public class InstanceRule<T> : IRule<T>
    {
        private readonly T _instance;

        public InstanceRule(T instance)
        {
            _instance = instance;
        }

        public T Resolve(IRuleResolver _)
        {
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

            if (obj is not InstanceRule<T> other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_instance);
        }

        protected bool Equals([NotNull] InstanceRule<T> other)
        {
            return EqualityComparer<T>.Default.Equals(_instance, other._instance);
        }
    }
}