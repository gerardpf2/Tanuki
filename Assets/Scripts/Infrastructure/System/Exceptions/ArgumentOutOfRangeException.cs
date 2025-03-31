using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Infrastructure.System.Exceptions
{
    public static class ArgumentOutOfRangeException
    {
        [NotNull] private static readonly Comparer Comparer = new();

        [ContractAnnotation("=> halt")]
        public static void Throw(object param, [CallerArgumentExpression("param")] string paramName = null)
        {
            throw new global::System.ArgumentOutOfRangeException(paramName, param, null);
        }

        public static void ThrowIfNot<T>([NotNull] T param, ComparisonOperator comparisonOperator, T value) where T : IComparable
        {
            ArgumentNullException.ThrowIfNull(param);

            if (!Comparer.IsTrueThat(param, comparisonOperator, value))
            {
                Throw(param);
            }
        }
    }
}