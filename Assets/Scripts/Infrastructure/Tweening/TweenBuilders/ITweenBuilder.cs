using System;
using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace Infrastructure.Tweening.TweenBuilders
{
    public interface ITweenBuilder<T>
    {
        [NotNull]
        ITweenBuilder<T> WithAutoPlay(bool autoPlay);

        [NotNull]
        ITweenBuilder<T> WithDelayBeforeS(float delayBeforeS);

        [NotNull]
        ITweenBuilder<T> WithDelayAfterS(float delayAfterS);

        [NotNull]
        ITweenBuilder<T> WithRepetitions(int repetitions);

        [NotNull]
        ITweenBuilder<T> WithRepetitionType(RepetitionType repetitionType);

        [NotNull]
        ITweenBuilder<T> WithDelayManagementRepetition(DelayManagement delayManagementRepetition);

        [NotNull]
        ITweenBuilder<T> WithDelayManagementRestart(DelayManagement delayManagementRestart);

        [NotNull]
        ITweenBuilder<T> WithOnStartIteration(Action onStartIteration);

        [NotNull]
        ITweenBuilder<T> WithOnPlay(Action onPlay);

        [NotNull]
        ITweenBuilder<T> WithOnRefresh(Action onRefresh);

        [NotNull]
        ITweenBuilder<T> WithOnEndIteration(Action onEndIteration);

        [NotNull]
        ITweenBuilder<T> WithOnPaused(Action onPaused);

        [NotNull]
        ITweenBuilder<T> WithOnCompleted(Action onCompleted);

        [NotNull]
        ITweenBuilder<T> WithStart(T start);

        [NotNull]
        ITweenBuilder<T> WithEnd(T end);

        [NotNull]
        ITweenBuilder<T> WithDurationS(float durationS);

        [NotNull]
        ITweenBuilder<T> WithSetter(Action<T> setter);

        [NotNull]
        ITweenBuilder<T> WithEasingMode(EasingMode easingMode);

        [NotNull]
        ITween Build();
    }
}