using System;
using JetBrains.Annotations;

namespace Infrastructure.Tweening.TweenBuilders
{
    public interface ISequenceAsyncBuilder
    {
        [NotNull]
        ISequenceAsyncBuilder WithAutoPlay(bool autoPlay);

        [NotNull]
        ISequenceAsyncBuilder WithDelayBeforeS(float delayBeforeS);

        [NotNull]
        ISequenceAsyncBuilder WithDelayAfterS(float delayAfterS);

        [NotNull]
        ISequenceAsyncBuilder WithRepetitions(int repetitions);

        [NotNull]
        ISequenceAsyncBuilder WithRepetitionType(RepetitionType repetitionType);

        [NotNull]
        ISequenceAsyncBuilder WithDelayManagementRepetition(DelayManagement delayManagementRepetition);

        [NotNull]
        ISequenceAsyncBuilder WithDelayManagementRestart(DelayManagement delayManagementRestart);

        [NotNull]
        ISequenceAsyncBuilder WithOnStartIteration(Action onStartIteration);

        [NotNull]
        ISequenceAsyncBuilder WithOnStartPlay(Action onStartPlay);

        [NotNull]
        ISequenceAsyncBuilder WithOnEndPlay(Action onEndPlay);

        [NotNull]
        ISequenceAsyncBuilder WithOnEndIteration(Action onEndIteration);

        [NotNull]
        ISequenceAsyncBuilder WithOnPaused(Action onPaused);

        [NotNull]
        ISequenceAsyncBuilder WithOnCompleted(Action onCompleted);

        [NotNull]
        ISequenceAsyncBuilder AddTween(ITween tween);

        [NotNull]
        ITween Build();
    }
}