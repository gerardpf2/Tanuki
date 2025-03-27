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
        ISequenceBuilder WithOnEndIteration(Action onEndIteration);

        [NotNull]
        ISequenceBuilder WithOnCompleted(Action onCompleted);

        [NotNull]
        ISequenceBuilder AddTween(ITween tween);

        [NotNull]
        ITween Build();
    }
}