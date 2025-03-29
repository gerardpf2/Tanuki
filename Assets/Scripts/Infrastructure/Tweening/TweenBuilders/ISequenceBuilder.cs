using System;
using JetBrains.Annotations;

namespace Infrastructure.Tweening.TweenBuilders
{
    public interface ISequenceBuilder
    {
        [NotNull]
        ISequenceBuilder WithAutoPlay(bool autoPlay);

        [NotNull]
        ISequenceBuilder WithDelayBeforeS(float delayBeforeS);

        [NotNull]
        ISequenceBuilder WithDelayAfterS(float delayAfterS);

        [NotNull]
        ISequenceBuilder WithRepetitions(int repetitions);

        [NotNull]
        ISequenceBuilder WithRepetitionType(RepetitionType repetitionType);

        [NotNull]
        ISequenceBuilder WithDelayManagementRepetition(DelayManagement delayManagementRepetition);

        [NotNull]
        ISequenceBuilder WithDelayManagementRestart(DelayManagement delayManagementRestart);

        [NotNull]
        ISequenceBuilder WithOnStartIteration(Action onStartIteration);

        [NotNull]
        ISequenceBuilder WithOnPlay(Action onPlay);

        [NotNull]
        ISequenceBuilder WithOnRefresh(Action onRefresh);

        [NotNull]
        ISequenceBuilder WithOnEndIteration(Action onEndIteration);

        [NotNull]
        ISequenceBuilder WithOnPaused(Action onPaused);

        [NotNull]
        ISequenceBuilder WithOnCompleted(Action onCompleted);

        [NotNull]
        ISequenceBuilder AddTween(ITween tween);

        [NotNull]
        ITween Build();
    }
}