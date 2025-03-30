using System;
using JetBrains.Annotations;

namespace Infrastructure.Tweening.TweenBuilders
{
    public abstract class TweenBaseBuilderHelper<TBuilder> : ITweenBaseBuilderHelper<TBuilder>
    {
        protected bool AutoPlay;
        protected float DelayBeforeS;
        protected float DelayAfterS;
        protected int Repetitions;
        protected RepetitionType RepetitionType;
        protected DelayManagement DelayManagementRepetition;
        protected DelayManagement DelayManagementRestart;
        protected Action OnStartIteration;
        protected Action OnStartPlay;
        protected Action OnEndPlay;
        protected Action OnEndIteration;
        protected Action OnPause;
        protected Action OnResume;
        protected Action OnRestart;
        protected Action OnComplete;

        [NotNull]
        protected abstract TBuilder This { get; }

        protected TweenBaseBuilderHelper()
        {
            Reset();
        }

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
            ITween tween = BuildTween();

            Reset();

            return tween;
        }

        private void Reset()
        {
            AutoPlay = true;
            DelayBeforeS = 0.0f;
            DelayAfterS = 0.0f;
            Repetitions = 0;
            RepetitionType = RepetitionType.Restart;
            DelayManagementRepetition = DelayManagement.BeforeAndAfter;
            DelayManagementRestart = DelayManagement.BeforeAndAfter;
            OnStartIteration = null;
            OnStartPlay = null;
            OnEndPlay = null;
            OnEndIteration = null;
            OnPause = null;
            OnComplete = null;

            CustomReset();
        }

        protected virtual void CustomReset() { }

        protected abstract ITween BuildTween();
    }
}