using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening.TweenBuilders
{
    public class SequenceBuilder : ISequenceBuilder
    {
        [NotNull, ItemNotNull] private readonly ICollection<ITween> _tweens = new List<ITween>();

        private bool _autoPlay;
        private float _delayBeforeS;
        private float _delayAfterS;
        private int _repetitions;
        private RepetitionType _repetitionType;
        private DelayManagement _delayManagementRepetition;
        private DelayManagement _delayManagementRestart;
        private Action _onStartIteration;
        private Action _onStartPlay;
        private Action _onEndPlay;
        private Action _onEndIteration;
        private Action _onPaused;
        private Action _onCompleted;

        public SequenceBuilder()
        {
            Reset();
        }

        public ISequenceBuilder WithAutoPlay(bool autoPlay)
        {
            _autoPlay = autoPlay;

            return this;
        }

        public ISequenceBuilder WithDelayBeforeS(float delayBeforeS)
        {
            _delayBeforeS = delayBeforeS;

            return this;
        }

        public ISequenceBuilder WithDelayAfterS(float delayAfterS)
        {
            _delayAfterS = delayAfterS;

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

        public ISequenceBuilder WithDelayManagementRepetition(DelayManagement delayManagementRepetition)
        {
            _delayManagementRepetition = delayManagementRepetition;

            return this;
        }

        public ISequenceBuilder WithDelayManagementRestart(DelayManagement delayManagementRestart)
        {
            _delayManagementRestart = delayManagementRestart;

            return this;
        }

        public ISequenceBuilder WithOnStartIteration(Action onStartIteration)
        {
            _onStartIteration = onStartIteration;

            return this;
        }

        public ISequenceBuilder WithOnStartPlay(Action onStartPlay)
        {
            _onStartPlay = onStartPlay;

            return this;
        }

        public ISequenceBuilder WithOnEndPlay(Action onEndPlay)
        {
            _onEndPlay = onEndPlay;

            return this;
        }

        public ISequenceBuilder WithOnEndIteration(Action onEndIteration)
        {
            _onEndIteration = onEndIteration;

            return this;
        }

        public ISequenceBuilder WithOnPaused(Action onPaused)
        {
            _onPaused = onPaused;

            return this;
        }

        public ISequenceBuilder WithOnCompleted(Action onCompleted)
        {
            _onCompleted = onCompleted;

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
                    _autoPlay,
                    _delayBeforeS,
                    _delayAfterS,
                    _repetitions,
                    _repetitionType,
                    _delayManagementRepetition,
                    _delayManagementRestart,
                    _onStartIteration,
                    _onStartPlay,
                    _onEndPlay,
                    _onEndIteration,
                    _onPaused,
                    _onCompleted,
                    new List<ITween>(_tweens)
                );

            Reset();

            return sequence;
        }

        private void Reset()
        {
            _autoPlay = SequenceBuilderDefaults.AutoPlay;
            _delayBeforeS = 0.0f;
            _delayAfterS = 0.0f;
            _repetitions = 0;
            _repetitionType = SequenceBuilderDefaults.RepetitionType;
            _delayManagementRepetition = SequenceBuilderDefaults.DelayManagementRepetition;
            _delayManagementRestart = SequenceBuilderDefaults.DelayManagementRestart;
            _onEndIteration = null;
            _onCompleted = null;
            _tweens.Clear();
        }
    }
}