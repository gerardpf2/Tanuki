using System;
using JetBrains.Annotations;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Tweening.Builders
{
    public abstract class TweenBaseBuilderHelper<TBuilder> : ITweenBaseBuilderHelper<TBuilder>
    {
        private bool _built;

        public bool AutoPlay { get; private set; } = TweenBaseBuilderConstants.AutoPlay;

        public float DelayBeforeS { get; private set; }

        public float DelayAfterS { get; private set; }

        public int Repetitions { get; private set; }

        public RepetitionType RepetitionType { get; private set; } = TweenBaseBuilderConstants.RepetitionType;

        public DelayManagement DelayManagementRepetition { get; private set; } = TweenBaseBuilderConstants.DelayManagementRepetition;

        public DelayManagement DelayManagementRestart { get; private set; } = TweenBaseBuilderConstants.DelayManagementRestart;

        public Action OnStartIteration { get; private set; }

        public Action OnStartPlay { get; private set; }

        public Action OnEndPlay { get; private set; }

        public Action OnEndIteration { get; private set; }

        public Action OnPause { get; private set; }

        public Action OnResume { get; private set; }

        public Action OnRestart { get; private set; }

        public Action OnComplete { get; private set; }

        [NotNull]
        protected abstract TBuilder This { get; }

        public TBuilder WithAutoPlay(bool autoPlay)
        {
            AutoPlay = autoPlay;

            return This;
        }

        public TBuilder WithDelayBeforeS(float delayBeforeS)
        {
            DelayBeforeS = delayBeforeS;

            return This;
        }

        public TBuilder WithDelayAfterS(float delayAfterS)
        {
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

        public ITween Build()
        {
            if (_built)
            {
                InvalidOperationException.Throw("Tween has already been built. Tween builders are not expected to be reused");
            }

            ITween tween = BuildTween();

            _built = true;

            return tween;
        }

        protected abstract ITween BuildTween();
    }
}