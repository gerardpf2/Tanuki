using System;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Infrastructure.Tweening
{
    public abstract class TweenBase : ITween
    {
        private readonly float _delayS;
        private readonly bool _autoPlay;
        private readonly int _repetitions;
        private readonly RepetitionType _repetitionType;
        private readonly Action _onIterationComplete;
        private readonly Action _onComplete;

        private TweenState _state = TweenState.SettingUp;
        private float _waitingTimeS;
        private int _iteration;

        public TweenState State
        {
            get => _state;
            private set
            {
                _state = value;

                CheckCallbackOnStateUpdated();
            }
        }

        protected bool Backwards { get; private set; }

        protected TweenBase(
            float delayS,
            bool autoPlay,
            int repetitions,
            RepetitionType repetitionType,
            Action onIterationComplete,
            Action onComplete)
        {
            _delayS = delayS;
            _autoPlay = autoPlay;
            _repetitions = repetitions;
            _repetitionType = repetitionType;
            _onIterationComplete = onIterationComplete;
            _onComplete = onComplete;
        }

        public float Update(float deltaTimeS, bool backwards = false)
        {
            switch (State)
            {
                case TweenState.SettingUp:
                    return ProcessSettingUpState(deltaTimeS, backwards);
                case TweenState.Waiting:
                    return ProcessWaitingState(deltaTimeS, backwards);
                case TweenState.Playing:
                    return ProcessPlayingState(deltaTimeS, backwards);
                case TweenState.CompletingIteration:
                    return ProcessCompletingIterationState(deltaTimeS, backwards);
                case TweenState.PreparingNextIteration:
                    return ProcessPreparingNextIterationState(deltaTimeS, backwards);
                case TweenState.Paused:
                    return 0.0f;
                case TweenState.Completed:
                    return deltaTimeS;
                default:
                    ArgumentOutOfRangeException.Throw(State);
                    return 0.0f;
            }
        }

        public bool Pause()
        {
            if (State is not (TweenState.Waiting or TweenState.Playing))
            {
                return false;
            }

            State = TweenState.Paused;

            return true;
        }

        public bool Resume()
        {
            if (State is not TweenState.Paused)
            {
                return false;
            }

            State = _waitingTimeS < _delayS ? TweenState.Waiting : TweenState.Playing;

            return true;
        }

        public virtual void Restart(bool withDelay)
        {
            RestartTimesAndUpdateState(withDelay);

            Backwards = false;

            _iteration = 0;
        }

        private void CheckCallbackOnStateUpdated()
        {
            // TODO

            switch (State)
            {
                case TweenState.SettingUp:
                    break;
                case TweenState.Waiting:
                    break;
                case TweenState.Playing:
                    break;
                case TweenState.CompletingIteration:
                    _onIterationComplete?.Invoke();
                    break;
                case TweenState.PreparingNextIteration:
                    break;
                case TweenState.Paused:
                    break;
                case TweenState.Completed:
                    _onComplete?.Invoke();
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(State);
                    return;
            }
        }

        private float ProcessSettingUpState(float deltaTimeS, bool backwards)
        {
            State = _autoPlay ? TweenState.Waiting : TweenState.Paused;

            return Update(deltaTimeS, backwards);
        }

        private float ProcessWaitingState(float deltaTimeS, bool backwards)
        {
            _waitingTimeS += deltaTimeS;

            if (_waitingTimeS < _delayS)
            {
                return 0.0f;
            }

            State = TweenState.Playing;

            deltaTimeS = _waitingTimeS - _delayS;

            return Update(deltaTimeS, backwards);
        }

        private float ProcessPlayingState(float deltaTimeS, bool backwards)
        {
            deltaTimeS = Refresh(deltaTimeS, backwards);

            if (deltaTimeS > 0.0f)
            {
                State = TweenState.CompletingIteration;

                deltaTimeS = Update(deltaTimeS, backwards);
            }

            return deltaTimeS;
        }

        private float ProcessCompletingIterationState(float deltaTimeS, bool backwards)
        {
            ++_iteration;

            if (_repetitions < 0 || _iteration <= _repetitions)
            {
                State = TweenState.PreparingNextIteration;
            }
            else
            {
                State = TweenState.Completed;
            }

            return Update(deltaTimeS, backwards);
        }

        private float ProcessPreparingNextIterationState(float deltaTimeS, bool backwards)
        {
            switch (_repetitionType)
            {
                case RepetitionType.Restart:
                    RestartForNextIteration(false);
                    break;
                case RepetitionType.RestartWithDelay:
                    RestartForNextIteration(true);
                    break;
                case RepetitionType.Yoyo:
                    RestartForNextIteration(false);
                    ToggleBackwards();
                    break;
                case RepetitionType.YoyoWithDelay:
                    RestartForNextIteration(true);
                    ToggleBackwards();
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(_repetitionType);
                    return 0.0f;
            }

            return Update(deltaTimeS, backwards);
        }

        private void RestartTimesAndUpdateState(bool withDelay)
        {
            State = withDelay ? TweenState.Waiting : TweenState.Playing;

            _waitingTimeS = withDelay ? 0.0f : _delayS;
        }

        protected abstract float Refresh(float deltaTimeS, bool backwards);

        protected virtual void RestartForNextIteration(bool withDelay)
        {
            RestartTimesAndUpdateState(withDelay);
        }

        private void ToggleBackwards()
        {
            Backwards = !Backwards;
        }
    }
}