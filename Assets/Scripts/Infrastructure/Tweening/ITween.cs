using Infrastructure.System;

namespace Infrastructure.Tweening
{
    public interface ITween
    {
        TweenState State { get; }

        bool Paused { get; }

        // Returns remaining deltaTimeS
        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f), Is(ComparisonOperator.LessThanOrEqualTo, "deltaTimeS")]
        float Step(float deltaTimeS, bool backwards = false);

        void Pause();

        void Resume();

        void Restart();

        // Returns remaining deltaTimeS
        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f), Is(ComparisonOperator.LessThanOrEqualTo, "deltaTimeS")]
        public float Update(float deltaTimeS, bool backwards = false)
        {
            while (deltaTimeS > 0.0f && State is not TweenState.Complete)
            {
                deltaTimeS = Step(deltaTimeS, backwards);
            }

            return deltaTimeS;
        }
    }
}