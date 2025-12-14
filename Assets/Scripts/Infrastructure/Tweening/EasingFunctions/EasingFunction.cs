using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening.EasingFunctions
{
    public class EasingFunction : IEasingFunction
    {
        [NotNull] private readonly Func<float, float> _getter;

        public EasingFunction([NotNull] Func<float, float> getter)
        {
            ArgumentNullException.ThrowIfNull(getter);

            _getter = getter;
        }

        public float Evaluate(float t)
        {
            return _getter(t);
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

            if (obj is not EasingFunction other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_getter);
        }

        private bool Equals([NotNull] EasingFunction other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return Equals(_getter, other._getter);
        }
    }
}