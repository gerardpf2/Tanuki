using System;
using JetBrains.Annotations;

namespace Infrastructure.Tweening.Builders
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
        TBuilder WithOnStep(Action onStep);

        [NotNull]
        TBuilder WithOnStartIteration(Action onStartIteration);

        [NotNull]
        TBuilder WithOnStartPlay(Action onStartPlay);

        [NotNull]
        TBuilder WithOnPlay(Action onPlay);

        [NotNull]
        TBuilder WithOnEndPlay(Action onEndPlay);

        [NotNull]
        TBuilder WithOnEndIteration(Action onEndIteration);

        [NotNull]
        TBuilder WithOnComplete(Action onComplete);

        [NotNull]
        TBuilder WithOnPause(Action onPause);

        [NotNull]
        TBuilder WithOnResume(Action onResume);

        [NotNull]
        TBuilder WithOnRestart(Action onRestart);

        [NotNull]
        ITweenBase Build();
    }
}