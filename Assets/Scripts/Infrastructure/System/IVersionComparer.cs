using System;

namespace Infrastructure.System
{
    public interface IVersionComparer
    {
        bool IsTrueThat(Version versionA, ComparisonOperator comparisonOperator, Version versionB);
    }
}