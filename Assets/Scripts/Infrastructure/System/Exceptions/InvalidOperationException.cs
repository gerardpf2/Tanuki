using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Infrastructure.System.Exceptions
{
    public static class InvalidOperationException
    {
        [NotNull] private static readonly Comparer Comparer = new();

        [ContractAnnotation("=> halt")]
        public static void Throw(string message = null)
        {
            throw new global::System.InvalidOperationException(message);
        }

        [ContractAnnotation("param:null => halt")]
        public static void ThrowIfNullWithMessage(object param, string message)
        {
            if (param == null) // "==" instead of "is" because of Unity's operator overloads
            {
                Throw(message);
            }
        }

        [ContractAnnotation("param:notnull => halt")]
        public static void ThrowIfNotNullWithMessage(object param, string message)
        {
            if (param != null) // "!=" instead of "is not" because of Unity's operator overloads
            {
                Throw(message);
            }
        }

        [ContractAnnotation("param:null => halt")]
        public static void ThrowIfNull(object param, [CallerArgumentExpression("param")] string paramName = null)
        {
            ThrowIfNullWithMessage(param, $"{paramName} cannot be null");
        }

        [ContractAnnotation("param:notnull => halt")]
        public static void ThrowIfNotNull(object param, [CallerArgumentExpression("param")] string paramName = null)
        {
            ThrowIfNotNullWithMessage(param, $"{paramName} must be null");
        }

        public static void ThrowIfNot<T>(
            [NotNull] T param,
            ComparisonOperator comparisonOperator,
            T value,
            [CallerArgumentExpression("param")] string paramName = null) where T : IComparable
        {
            ArgumentNullException.ThrowIfNull(param);

            if (!Comparer.IsTrueThat(param, comparisonOperator, value))
            {
                Throw($"{paramName} with value {param} is not {comparisonOperator} {value}");
            }
        }
    }
}