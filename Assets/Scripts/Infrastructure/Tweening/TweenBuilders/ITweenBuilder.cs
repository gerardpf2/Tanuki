using System;
using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace Infrastructure.Tweening.TweenBuilders
{
    public interface ITweenBuilder<T>
    {
        [NotNull]
        ITweenBuilder<T> WithStart(T start);

        [NotNull]
        ITweenBuilder<T> WithEnd(T end);

        [NotNull]
        ITweenBuilder<T> WithDelayS(float delayS);

        [NotNull]
        ITweenBuilder<T> WithDurationS(float durationS);

        [NotNull]
        ITweenBuilder<T> WithRepetitions(int repetitions);

        [NotNull]
        ITweenBuilder<T> WithRepetitionType(RepetitionType repetitionType);

        [NotNull]
        ITweenBuilder<T> WithOnIterationComplete(Action onIterationComplete);

        [NotNull]
        ITweenBuilder<T> WithOnComplete(Action onComplete);

        [NotNull]
        ITweenBuilder<T> WithSetter(Action<T> setter);

        [NotNull]
        ITweenBuilder<T> WithEasingMode(EasingMode easingMode);

        [NotNull]
        ITween Build();
    }
}