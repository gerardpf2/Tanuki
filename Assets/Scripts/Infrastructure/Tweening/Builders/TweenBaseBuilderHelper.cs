using System;
using JetBrains.Annotations;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Tweening.Builders
{
    public abstract class TweenBaseBuilderHelper<TBuilder, TTween> : ITweenBaseBuilderHelper<TBuilder, TTween>
    {
        private bool _built;

        public bool AutoPlay { get; private set; } = TweenBaseBuilderConstants.AutoPlay;

        public float DelayBeforeS { get; private set; }

        public float DelayAfterS { get; private set; }

        public int Repetitions { get; private set; }

        public RepetitionType RepetitionType { get; private set; } = TweenBaseBuilderConstants.RepetitionType;

        public DelayManagement DelayManagementRepetition { get; private set; } = TweenBaseBuilderConstants.DelayManagementRepetition;

        public DelayManagement DelayManagementRestart { get; private set; } = TweenBaseBuilderConstants.DelayManagementRestart;

        public Action<TTween> OnStep { get; private set; }

        public Action<TTween> OnStartIteration { get; private set; }

        public Action<TTween> OnStartPlay { get; private set; }

        public Action<TTween> OnPlay { get; private set; }

        public Action<TTween> OnEndPlay { get; private set; }

        public Action<TTween> OnEndIteration { get; private set; }

        public Action<TTween> OnComplete { get; private set; }

        public Action<TTween> OnPause { get; private set; }

        public Action<TTween> OnResume { get; private set; }

        public Action<TTween> OnRestart { get; private set; }

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

        public TBuilder WithOnStep(Action<TTween> onStep)
        {
            OnStep = onStep;

            return This;
        }

        public TBuilder WithOnStartIteration(Action<TTween> onStartIteration)
        {
            OnStartIteration = onStartIteration;

            return This;
        }

        public TBuilder WithOnStartPlay(Action<TTween> onStartPlay)
        {
            OnStartPlay = onStartPlay;

            return This;
        }

        public TBuilder WithOnPlay(Action<TTween> onPlay)
        {
            OnPlay = onPlay;

            return This;
        }

        public TBuilder WithOnEndPlay(Action<TTween> onEndPlay)
        {
            OnEndPlay = onEndPlay;

            return This;
        }

        public TBuilder WithOnEndIteration(Action<TTween> onEndIteration)
        {
            OnEndIteration = onEndIteration;

            return This;
        }

        public TBuilder WithOnComplete(Action<TTween> onComplete)
        {
            OnComplete = onComplete;

            return This;
        }

        public TBuilder WithOnPause(Action<TTween> onPause)
        {
            OnPause = onPause;

            return This;
        }

        public TBuilder WithOnResume(Action<TTween> onResume)
        {
            OnResume = onResume;

            return This;
        }

        public TBuilder WithOnRestart(Action<TTween> onRestart)
        {
            OnRestart = onRestart;

            return This;
        }

        public ITweenBase Build()
        {
            if (_built)
            {
                InvalidOperationException.Throw("Tween has already been built. Tween builders are not expected to be reused");
            }

            ITweenBase tween = BuildTween();

            _built = true;

            return tween;
        }

        protected abstract ITweenBase BuildTween();
    }
}