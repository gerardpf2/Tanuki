using Infrastructure.System;

namespace Infrastructure.Tweening
{
    public interface ITween
    {
        TweenState State { get; }

        // Returns remaining deltaTimeS
        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f), Is(ComparisonOperator.LessThanOrEqualTo, "deltaTimeS")]
        float Update(float deltaTimeS, bool backwards = false);

        bool Pause();

        bool Resume();

        bool Restart();
    }
}