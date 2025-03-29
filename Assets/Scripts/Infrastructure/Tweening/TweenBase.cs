using System;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Infrastructure.Tweening
{
    public abstract class TweenBase : ITween
    {
        private readonly bool _autoPlay;
        private readonly float _delayBeforeS;
        private readonly float _delayAfterS;
        private readonly int _repetitions;
        private readonly RepetitionType _repetitionType;
        private readonly DelayManagement _delayManagementRepetition;
        private readonly DelayManagement _delayManagementRestart;
        private readonly Action _onStartIteration;
        private readonly Action _onPlay;
        private readonly Action _onRefresh;
        private readonly Action _onEndIteration;
        private readonly Action _onPaused;
        private readonly Action _onCompleted;

        private DelayManagement _delayManagement = DelayManagement.BeforeAndAfter;
        private TweenState _state = TweenState.SetUp;
        private float _waitTimeS;
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
            bool autoPlay,
            float delayBeforeS,
            float delayAfterS,
            int repetitions,
            RepetitionType repetitionType,
            DelayManagement delayManagementRepetition,
            DelayManagement delayManagementRestart,
            Action onStartIteration,
            Action onPlay,
            Action onRefresh,
            Action onEndIteration,
            Action onPaused,
            Action onCompleted)
        {
            _autoPlay = autoPlay;
            _delayBeforeS = delayBeforeS;
            _delayAfterS = delayAfterS;
            _repetitions = repetitions;
            _repetitionType = repetitionType;
            _delayManagementRepetition = delayManagementRepetition;
            _delayManagementRestart = delayManagementRestart;
            _onStartIteration = onStartIteration;
            _onPlay = onPlay;
            _onRefresh = onRefresh;
            _onEndIteration = onEndIteration;
            _onPaused = onPaused;
            _onCompleted = onCompleted;
        }

        public float Update(float deltaTimeS, bool backwards = false)
        {
            while (deltaTimeS > 0.0f)
            {
                switch (State)
                {
                    case TweenState.SetUp:
                        ProcessSetUp();
                        break;
                    case TweenState.StartIteration:
                        ProcessStartIteration();
                        break;
                    case TweenState.WaitBefore:
                        deltaTimeS = ProcessWaitBefore(deltaTimeS);
                        break;
                    case TweenState.Play:
                        deltaTimeS = ProcessPlay(deltaTimeS, backwards);
                        break;
                    case TweenState.WaitAfter:
                        deltaTimeS = ProcessWaitAfter(deltaTimeS);
                        break;
                    case TweenState.EndIteration:
                        ProcessEndIteration();
                        break;
                    case TweenState.Paused:
                        return 0.0f;
                    case TweenState.Completed:
                        return deltaTimeS;
                    default:
                        ArgumentOutOfRangeException.Throw(State);
                        return 0.0f;
                }
            }

            return deltaTimeS;
        }

        public bool Pause()
        {
            if (State is TweenState.Paused)
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

            // TODO

            return true;
        }

        public virtual void Restart()
        {
            State = TweenState.SetUp;

            Backwards = false;

            _delayManagement = _delayManagementRestart;
            _waitTimeS = 0.0f;
            _iteration = 0;
        }

        private void CheckCallbackOnStateUpdated()
        {
            switch (State)
            {
                case TweenState.SetUp:
                    break;
                case TweenState.StartIteration:
                    _onStartIteration?.Invoke();
                    break;
                case TweenState.WaitBefore:
                    break;
                case TweenState.Play:
                    _onPlay?.Invoke();
                    break;
                case TweenState.WaitAfter:
                    break;
                case TweenState.EndIteration:
                    _onEndIteration?.Invoke();
                    break;
                case TweenState.Paused:
                    _onPaused?.Invoke();
                    break;
                case TweenState.Completed:
                    _onCompleted?.Invoke();
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(State);
                    return;
            }
        }

        private void ProcessSetUp()
        {
            State = _autoPlay ? TweenState.StartIteration : TweenState.Paused;
        }

        private void ProcessStartIteration()
        {
            State = _delayManagement is DelayManagement.BeforeAndAfter or DelayManagement.Before ?
                TweenState.WaitBefore :
                TweenState.Play;
        }

        private float ProcessWaitBefore(float deltaTimeS)
        {
            return ProcessWait(deltaTimeS, _delayBeforeS, TweenState.Play);
        }

        private float ProcessPlay(float deltaTimeS, bool backwards)
        {
            _onRefresh?.Invoke();

            deltaTimeS = Refresh(deltaTimeS, backwards);

            if (deltaTimeS > 0.0f)
            {
                State = _delayManagement is DelayManagement.BeforeAndAfter or DelayManagement.After ?
                    TweenState.WaitAfter :
                    TweenState.EndIteration;
            }

            return deltaTimeS;
        }

        private float ProcessWaitAfter(float deltaTimeS)
        {
            return ProcessWait(deltaTimeS, _delayAfterS, TweenState.EndIteration);
        }

        private void ProcessEndIteration()
        {
            ++_iteration;

            if (_repetitions < 0 || _iteration <= _repetitions)
            {
                PrepareRepetition();
            }
            else
            {
                State = TweenState.Completed;
            }
        }

        private float ProcessWait(float deltaTimeS, float delayS, TweenState nextState)
        {
            _waitTimeS += deltaTimeS;

            if (_waitTimeS < delayS)
            {
                return 0.0f;
            }

            State = nextState;

            float remainingDeltaTimeS = _waitTimeS - delayS;

            _waitTimeS = 0.0f;

            return remainingDeltaTimeS;
        }

        protected abstract float Refresh(float deltaTimeS, bool backwards);

        protected virtual void PrepareRepetition()
        {
            State = TweenState.StartIteration;

            if (_repetitionType is RepetitionType.Yoyo)
            {
                Backwards = !Backwards;
            }

            _delayManagement = _delayManagementRepetition;
        }
    }
}