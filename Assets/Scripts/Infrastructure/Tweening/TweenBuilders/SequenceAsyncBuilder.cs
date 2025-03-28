using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening.TweenBuilders
{
    public class SequenceAsyncBuilder : ISequenceAsyncBuilder
    {
        [NotNull, ItemNotNull] private readonly ICollection<ITween> _tweens = new List<ITween>();

        private bool _autoPlay;
        private float _delayBeforeS;
        private float _delayAfterS;
        private int _repetitions;
        private RepetitionType _repetitionType;
        private DelayManagement _delayManagementRepetition;
        private DelayManagement _delayManagementRestart;
        private Action _onEndIteration;
        private Action _onCompleted;

        public SequenceAsyncBuilder()
        {
            Reset();
        }

        public ISequenceAsyncBuilder WithAutoPlay(bool autoPlay)
        {
            _autoPlay = autoPlay;

            return this;
        }

        public ISequenceAsyncBuilder WithDelayBeforeS(float delayBeforeS)
        {
            _delayBeforeS = delayBeforeS;

            return this;
        }

        public ISequenceAsyncBuilder WithDelayAfterS(float delayAfterS)
        {
            _delayAfterS = delayAfterS;

            return this;
        }

        public ISequenceAsyncBuilder WithRepetitions(int repetitions)
        {
            _repetitions = repetitions;

            return this;
        }

        public ISequenceAsyncBuilder WithRepetitionType(RepetitionType repetitionType)
        {
            _repetitionType = repetitionType;

            return this;
        }

        public ISequenceAsyncBuilder WithDelayManagementRepetition(DelayManagement delayManagementRepetition)
        {
            _delayManagementRepetition = delayManagementRepetition;

            return this;
        }

        public ISequenceAsyncBuilder WithDelayManagementRestart(DelayManagement delayManagementRestart)
        {
            _delayManagementRestart = delayManagementRestart;

            return this;
        }

        public ISequenceAsyncBuilder WithOnEndIteration(Action onEndIteration)
        {
            _onEndIteration = onEndIteration;

            return this;
        }

        public ISequenceAsyncBuilder WithOnCompleted(Action onCompleted)
        {
            _onCompleted = onCompleted;

            return this;
        }

        public ISequenceAsyncBuilder AddTween([NotNull] ITween tween)
        {
            ArgumentNullException.ThrowIfNull(tween);

            _tweens.Add(tween);

            return this;
        }

        public ITween Build()
        {
            ITween sequence =
                new SequenceAsync(
                    _autoPlay,
                    _delayBeforeS,
                    _delayAfterS,
                    _repetitions,
                    _repetitionType,
                    _delayManagementRepetition,
                    _delayManagementRestart,
                    _onEndIteration,
                    _onCompleted,
                    new List<ITween>(_tweens)
                );

            Reset();

            return sequence;
        }

        private void Reset()
        {
            _autoPlay = SequenceAsyncBuilderDefaults.AutoPlay;
            _delayBeforeS = 0.0f;
            _delayAfterS = 0.0f;
            _repetitions = 0;
            _repetitionType = SequenceAsyncBuilderDefaults.RepetitionType;
            _delayManagementRepetition = SequenceAsyncBuilderDefaults.DelayManagementRepetition;
            _delayManagementRestart = SequenceAsyncBuilderDefaults.DelayManagementRestart;
            _onEndIteration = null;
            _onCompleted = null;
            _tweens.Clear();
        }
    }
}