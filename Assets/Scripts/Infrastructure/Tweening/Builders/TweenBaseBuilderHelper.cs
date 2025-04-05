using System;
using Infrastructure.System;
using JetBrains.Annotations;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Tweening.Builders
{
    public abstract class TweenBaseBuilderHelper<TBuilder> : ITweenBaseBuilderHelper<TBuilder>
    {
        private float _delayBeforeS;
        private float _delayAfterS;

        protected bool AutoPlay { get; private set; } = true;

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)]
        protected float DelayBeforeS
        {
            get
            {
                InvalidOperationException.ThrowIfNot(_delayBeforeS, ComparisonOperator.GreaterThanOrEqualTo, 0.0f);

                return _delayBeforeS;
            }
            private set => _delayBeforeS = value;
        }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)]
        protected float DelayAfterS
        {
            get
            {
                InvalidOperationException.ThrowIfNot(_delayAfterS, ComparisonOperator.GreaterThanOrEqualTo, 0.0f);

                return _delayAfterS;
            }
            private set => _delayAfterS = value;
        }

        protected int Repetitions { get; private set; }

        protected RepetitionType RepetitionType { get; private set; } = RepetitionType.Restart;

        protected DelayManagement DelayManagementRepetition { get; private set; } = DelayManagement.BeforeAndAfter;

        protected DelayManagement DelayManagementRestart { get; private set; } = DelayManagement.BeforeAndAfter;

        protected Action OnStartIteration { get; private set; }

        protected Action OnStartPlay { get; private set; }

        protected Action OnEndPlay { get; private set; }

        protected Action OnEndIteration { get; private set; }

        protected Action OnPause { get; private set; }

        protected Action OnResume { get; private set; }

        protected Action OnRestart { get; private set; }

        protected Action OnComplete { get; private set; }

        [NotNull]
        protected abstract TBuilder This { get; }

        public TBuilder WithAutoPlay(bool autoPlay)
        {
            AutoPlay = autoPlay;

            return This;
        }

        public TBuilder WithDelayBeforeS([Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)] float delayBeforeS)
        {
            ArgumentOutOfRangeException.ThrowIfNot(delayBeforeS, ComparisonOperator.GreaterThanOrEqualTo, 0.0f);

            DelayBeforeS = delayBeforeS;

            return This;
        }

        public TBuilder WithDelayAfterS([Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)] float delayAfterS)
        {
            ArgumentOutOfRangeException.ThrowIfNot(delayAfterS, ComparisonOperator.GreaterThanOrEqualTo, 0.0f);

            DelayAfterS = delayAfterS;

            return This;
        }

        public TBuilder WithRepetitions(int repetitions)
        {
            Repetitions = repetitions;

            return This;
        }

        public TBuilder WithRepetitionType(RepetitionType repetitionType)
        {
            RepetitionType = repetitionType;

            return This;
        }

        public TBuilder WithDelayManagementRepetition(DelayManagement delayManagementRepetition)
        {
            DelayManagementRepetition = delayManagementRepetition;

            return This;
        }

        public TBuilder WithDelayManagementRestart(DelayManagement delayManagementRestart)
        {
            DelayManagementRestart = delayManagementRestart;

            return This;
        }

        public TBuilder WithOnStartIteration(Action onStartIteration)
        {
            OnStartIteration = onStartIteration;

            return This;
        }

        public TBuilder WithOnStartPlay(Action onStartPlay)
        {
            OnStartPlay = onStartPlay;

            return This;
        }

        public TBuilder WithOnEndPlay(Action onEndPlay)
        {
            OnEndPlay = onEndPlay;

            return This;
        }

        public TBuilder WithOnEndIteration(Action onEndIteration)
        {
            OnEndIteration = onEndIteration;

            return This;
        }

        public TBuilder WithOnPause(Action onPause)
        {
            OnPause = onPause;

            return This;
        }

        public TBuilder WithOnResume(Action onResume)
        {
            OnResume = onResume;

            return This;
        }

        public TBuilder WithOnRestart(Action onRestart)
        {
            OnRestart = onRestart;

            return This;
        }

        public TBuilder WithOnComplete(Action onComplete)
        {
            OnComplete = onComplete;

            return This;
        }

        public abstract ITween Build();
    }
}