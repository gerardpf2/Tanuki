using System;

namespace Infrastructure.System
{
    public interface IComparer
    {
        bool IsTrueThat<T>(T valueA, ComparisonOperator comparisonOperator, T valueB) where T : IComparable;
    }
}