using System;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Tweening
{
    public abstract class TweenBase : ITween
    {
        // TODO: Move callbacks to State set and add missing ones

        private readonly float _delayS;
        private readonly bool _autoPlay;
        private readonly int _repetitions;
        private readonly RepetitionType _repetitionType;
        private readonly Action _onIterationComplete;
        private readonly Action _onComplete;

        private TweenState _state = TweenState.SettingUp;
        private float _waitingTimeS;
        private float _playingTimeS;
        private bool _backwards;
        private int _iteration;

        public TweenState State
        {
            get => _state;
            private set
            {
                if (State == value)
                {
                    InvalidOperationException.Throw(); // TODO
                }

                _state = value;
            }
        }

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

        public void Update(float deltaTimeS, bool backwards = false)
        {
            switch (State)
            {
                case TweenState.SettingUp:
                    ProcessSettingUpState(deltaTimeS, backwards);
                    break;
                case TweenState.Waiting:
                    ProcessWaitingState(deltaTimeS, backwards);
                    break;
                case TweenState.Playing:
                    ProcessPlayingState(deltaTimeS, backwards);
                    break;
                case TweenState.CompletingIteration:
                    ProcessCompletingIterationState(deltaTimeS, backwards);
                    break;
                case TweenState.PreparingNextIteration:
                    ProcessPreparingNextIterationState(deltaTimeS, backwards);
                    break;
                case TweenState.Completing:
                    ProcessCompletingState(deltaTimeS, backwards);
                    break;
                case TweenState.Paused:
                case TweenState.Completed:
                    return;
                default:
                    ArgumentOutOfRangeException.Throw(State);
                    return;
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

            _backwards = false;
            _iteration = 0;
        }

        private void ProcessSettingUpState(float deltaTimeS, bool backwards)
        {
            State = _autoPlay ? TweenState.Waiting : TweenState.Paused;

            Update(deltaTimeS, backwards);
        }

        private void ProcessWaitingState(float deltaTimeS, bool backwards)
        {
            _waitingTimeS += deltaTimeS;

            if (_waitingTimeS < _delayS)
            {
                return;
            }

            State = TweenState.Playing;

            Update(_waitingTimeS - _delayS, backwards);
        }

        private void ProcessPlayingState(float deltaTimeS, bool backwards)
        {
            _playingTimeS += deltaTimeS;

            // TODO: TryRefresh and remove _playingTimeS

            if (CanRefresh(_playingTimeS))
            {
                Refresh(deltaTimeS, _playingTimeS, backwards ^ _backwards);
            }
            else
            {
                State = TweenState.CompletingIteration;

                Update(deltaTimeS, backwards);
            }
        }

        private void ProcessCompletingIterationState(float deltaTimeS, bool backwards)
        {
            ++_iteration;

            _onIterationComplete?.Invoke();

            CompleteIteration(backwards ^ _backwards);

            if (_repetitions < 0 || _iteration <= _repetitions)
            {
                State = TweenState.PreparingNextIteration;
            }
            else
            {
                State = TweenState.Completing;
            }

            Update(deltaTimeS, backwards);
        }

        private void ProcessPreparingNextIterationState(float deltaTimeS, bool backwards)
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
                    return;
            }

            Update(deltaTimeS, backwards);
        }

        private void ProcessCompletingState(float deltaTimeS, bool backwards)
        {
            State = TweenState.Completed;

            _onComplete?.Invoke();

            Update(deltaTimeS, backwards);
        }

        private void RestartTimesAndUpdateState(bool withDelay)
        {
            State = withDelay ? TweenState.Waiting : TweenState.Playing;

            _waitingTimeS = withDelay ? 0.0f : _delayS;
            _playingTimeS = 0.0f;
        }

        protected abstract bool CanRefresh(float playingTimeS);

        protected abstract void Refresh(float deltaTimeS, float playingTimeS, bool backwards);

        protected virtual void CompleteIteration(bool backwards) { }

        protected virtual void RestartForNextIteration(bool withDelay)
        {
            RestartTimesAndUpdateState(withDelay);
        }

        private void ToggleBackwards()
        {
            _backwards = !_backwards;
        }
    }
}