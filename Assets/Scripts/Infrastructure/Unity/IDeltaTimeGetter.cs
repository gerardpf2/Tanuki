using Infrastructure.System;

namespace Infrastructure.Unity
{
    public interface IDeltaTimeGetter
    {
        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)]
        float Get();
    }
}