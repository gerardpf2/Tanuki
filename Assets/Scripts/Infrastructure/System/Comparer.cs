using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Infrastructure.System
{
    public class Comparer : IComparer
    {
        // TODO: Test
        public bool IsTrueThat<T>([NotNull] T valueA, ComparisonOperator comparisonOperator, T valueB) where T : IComparable
        {
            ArgumentNullException.ThrowIfNull(valueA);

            int result = valueA.CompareTo(valueB);

            switch (comparisonOperator)
            {
                case ComparisonOperator.EqualTo:
                    return result == 0;
                case ComparisonOperator.UnequalTo:
                    return result != 0;
                case ComparisonOperator.LessThan:
                    return result < 0;
                case ComparisonOperator.GreaterThan:
                    return result > 0;
                case ComparisonOperator.LessThanOrEqualTo:
                    return result <= 0;
                case ComparisonOperator.GreaterThanOrEqualTo:
                    return result >= 0;
                default:
                    ArgumentOutOfRangeException.Throw(comparisonOperator);
                    return false;
            }
        }
    }
}