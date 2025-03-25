using System;
using JetBrains.Annotations;

namespace Infrastructure.Tweening.TweenBuilders
{
    public interface ISequenceBuilder
    {
        [NotNull]
        ISequenceBuilder WithDelayS(float delayS);

        [NotNull]
        ISequenceBuilder WithAutoPlay(bool autoPlay);

        [NotNull]
        ISequenceBuilder WithRepetitions(int repetitions);

        [NotNull]
        ISequenceBuilder WithRepetitionType(RepetitionType repetitionType);

        [NotNull]
        ISequenceBuilder WithOnIterationComplete(Action onIterationComplete);

        [NotNull]
        ISequenceBuilder WithOnComplete(Action onComplete);

        [NotNull]
        ISequenceBuilder AddTween(ITween tween);

        [NotNull]
        ITween Build();
    }
}