using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening.TweenBuilders
{
    public class SequenceBuilder : ISequenceBuilder
    {
        [NotNull, ItemNotNull] private readonly ICollection<ITween> _tweens = new List<ITween>();

        private float _delayS;
        private bool _autoPlay;
        private int _repetitions;
        private RepetitionType _repetitionType;
        private Action _onIterationComplete;
        private Action _onComplete;

        public SequenceBuilder()
        {
            Reset();
        }

        public ISequenceBuilder WithDelayS(float delayS)
        {
            _delayS = delayS;

            return this;
        }

        public ISequenceBuilder WithAutoPlay(bool autoPlay)
        {
            _autoPlay = autoPlay;

            return this;
        }

        public ISequenceBuilder WithRepetitions(int repetitions)
        {
            _repetitions = repetitions;

            return this;
        }

        public ISequenceBuilder WithRepetitionType(RepetitionType repetitionType)
        {
            _repetitionType = repetitionType;

            return this;
        }

        public ISequenceBuilder WithOnIterationComplete(Action onIterationComplete)
        {
            _onIterationComplete = onIterationComplete;

            return this;
        }

        public ISequenceBuilder WithOnComplete(Action onComplete)
        {
            _onComplete = onComplete;

            return this;
        }

        public ISequenceBuilder AddTween([NotNull] ITween tween)
        {
            ArgumentNullException.ThrowIfNull(tween);

            _tweens.Add(tween);

            return this;
        }

        public ITween Build()
        {
            ITween sequence =
                new Sequence(
                    _delayS,
                    _autoPlay,
                    _repetitions,
                    _repetitionType,
                    _onIterationComplete,
                    _onComplete,
                    new List<ITween>(_tweens)
                );

            Reset();

            return sequence;
        }

        private void Reset()
        {
            _delayS = 0.0f;
            _autoPlay = SequenceBuilderDefaults.AutoPlay;
            _repetitions = 0;
            _repetitionType = SequenceBuilderDefaults.RepetitionType;
            _onIterationComplete = null;
            _onComplete = null;
            _tweens.Clear();
        }
    }
}