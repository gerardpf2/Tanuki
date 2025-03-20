using System;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Infrastructure.System
{
    public class VersionComparer : IVersionComparer
    {
        public bool IsTrueThat(Version versionA, ComparisonOperator comparisonOperator, Version versionB)
        {
            switch (comparisonOperator)
            {
                case ComparisonOperator.EqualTo:
                    return versionA == versionB;
                case ComparisonOperator.UnequalTo:
                    return versionA != versionB;
                case ComparisonOperator.LessThan:
                    return versionA < versionB;
                case ComparisonOperator.GreaterThan:
                    return versionA > versionB;
                case ComparisonOperator.LessThanOrEqualTo:
                    return versionA <= versionB;
                case ComparisonOperator.GreaterThanOrEqualTo:
                    return versionA >= versionB;
                default:
                    ArgumentOutOfRangeException.Throw(comparisonOperator);
                    return false;
            }
        }
    }
}