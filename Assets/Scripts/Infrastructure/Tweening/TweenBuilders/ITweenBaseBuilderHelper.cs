using System;
using JetBrains.Annotations;

namespace Infrastructure.Tweening.TweenBuilders
{
    public interface ITweenBaseBuilderHelper<out TBuilder>
    {
        [NotNull]
        TBuilder WithAutoPlay(bool autoPlay);

        [NotNull]
        TBuilder WithDelayBeforeS(float delayBeforeS);

        [NotNull]
        TBuilder WithDelayAfterS(float delayAfterS);

        [NotNull]
        TBuilder WithRepetitions(int repetitions);

        [NotNull]
        TBuilder WithRepetitionType(RepetitionType repetitionType);

        [NotNull]
        TBuilder WithDelayManagementRepetition(DelayManagement delayManagementRepetition);

        [NotNull]
        TBuilder WithDelayManagementRestart(DelayManagement delayManagementRestart);

        [NotNull]
        TBuilder WithOnStartIteration(Action onStartIteration);

        [NotNull]
        TBuilder WithOnStartPlay(Action onStartPlay);

        [NotNull]
        TBuilder WithOnEndPlay(Action onEndPlay);

        [NotNull]
        TBuilder WithOnEndIteration(Action onEndIteration);

        [NotNull]
        TBuilder WithOnPaused(Action onPaused);

        [NotNull]
        TBuilder WithOnCompleted(Action onCompleted);

        [NotNull]
        ITween Build();
    }
}