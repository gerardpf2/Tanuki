using System;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

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
        private readonly Action _onStartPlay;
        private readonly Action _onEndPlay;
        private readonly Action _onEndIteration;
        private readonly Action _onPause;
        private readonly Action _onResume;
        private readonly Action _onRestart;
        private readonly Action _onComplete;

        private DelayManagement _delayManagement = DelayManagement.BeforeAndAfter;
        private TweenState _state = TweenState.SetUp;
        private TweenState? _stateBeforePause;
        private float _waitTimeS;
        private bool _setupDone;
        private int _iteration;

        public TweenState State
        {
            get => _state;
            private set
            {
                if (value is TweenState.Pause)
                {
                    _stateBeforePause = State;
                }

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
            Action onStartPlay,
            Action onEndPlay,
            Action onEndIteration,
            Action onPause,
            Action onResume,
            Action onRestart,
            Action onComplete)
        {
            _autoPlay = autoPlay;
            _delayBeforeS = delayBeforeS;
            _delayAfterS = delayAfterS;
            _repetitions = repetitions;
            _repetitionType = repetitionType;
            _delayManagementRepetition = delayManagementRepetition;
            _delayManagementRestart = delayManagementRestart;
            _onStartIteration = onStartIteration;
            _onStartPlay = onStartPlay;
            _onEndPlay = onEndPlay;
            _onEndIteration = onEndIteration;
            _onPause = onPause;
            _onResume = onResume;
            _onRestart = onRestart;
            _onComplete = onComplete;
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
                    case TweenState.StartPlay:
                        ProcessStartPlay();
                        break;
                    case TweenState.Play:
                        deltaTimeS = ProcessPlay(deltaTimeS, backwards);
                        break;
                    case TweenState.EndPlay:
                        ProcessEndPlay();
                        break;
                    case TweenState.WaitAfter:
                        deltaTimeS = ProcessWaitAfter(deltaTimeS);
                        break;
                    case TweenState.EndIteration:
                        ProcessEndIteration();
                        break;
                    case TweenState.Pause:
                        return 0.0f;
                    case TweenState.Resume:
                        ProcessResume();
                        break;
                    case TweenState.Restart:
                        ProcessRestart();
                        break;
                    case TweenState.Complete:
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
            if (State is not (TweenState.WaitBefore or TweenState.Play or TweenState.WaitAfter))
            {
                return false;
            }

            State = TweenState.Pause;

            return true;
        }

        public bool Resume()
        {
            if (State is not TweenState.Pause)
            {
                return false;
            }

            State = TweenState.Resume;

            return true;
        }

        public void Restart()
        {
            State = TweenState.Restart;
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
                case TweenState.StartPlay:
                    _onStartPlay?.Invoke();
                    break;
                case TweenState.Play:
                    break;
                case TweenState.EndPlay:
                    _onEndPlay?.Invoke();
                    break;
                case TweenState.WaitAfter:
                    break;
                case TweenState.EndIteration:
                    _onEndIteration?.Invoke();
                    break;
                case TweenState.Pause:
                    _onPause?.Invoke();
                    break;
                case TweenState.Resume:
                    _onResume?.Invoke();
                    break;
                case TweenState.Restart:
                    _onRestart?.Invoke();
                    break;
                case TweenState.Complete:
                    _onComplete?.Invoke();
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(State);
                    return;
            }
        }

        private void ProcessSetUp()
        {
            State = _autoPlay || _setupDone ? TweenState.StartIteration : TweenState.Pause;

            _setupDone = true;
        }

        private void ProcessStartIteration()
        {
            State = _delayManagement is DelayManagement.BeforeAndAfter or DelayManagement.Before ?
                TweenState.WaitBefore :
                TweenState.StartPlay;
        }

        private float ProcessWaitBefore(float deltaTimeS)
        {
            return ProcessWait(deltaTimeS, _delayBeforeS, TweenState.StartPlay);
        }

        private void ProcessStartPlay()
        {
            State = TweenState.Play;
        }

        private float ProcessPlay(float deltaTimeS, bool backwards)
        {
            deltaTimeS = Play(deltaTimeS, backwards);

            if (deltaTimeS > 0.0f)
            {
                State = TweenState.EndPlay;
            }

            return deltaTimeS;
        }

        private void ProcessEndPlay()
        {
            State = _delayManagement is DelayManagement.BeforeAndAfter or DelayManagement.After ?
                TweenState.WaitAfter :
                TweenState.EndIteration;
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
                State = TweenState.Complete;
            }
        }

        private void ProcessResume()
        {
            InvalidOperationException.ThrowIfNull(_stateBeforePause);

            State = _stateBeforePause.Value;

            _stateBeforePause = null;
        }

        private void ProcessRestart()
        {
            State = TweenState.StartIteration;

            Backwards = false;

            _delayManagement = _delayManagementRestart;
            _stateBeforePause = null;
            _waitTimeS = 0.0f;
            _iteration = 0;

            OnRestart();
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

        protected abstract float Play(float deltaTimeS, bool backwards);

        private void PrepareRepetition()
        {
            State = TweenState.StartIteration;

            if (_repetitionType is RepetitionType.Yoyo)
            {
                Backwards = !Backwards;
            }

            _delayManagement = _delayManagementRepetition;

            OnPrepareRepetition();
        }

        protected virtual void OnRestart() { }

        protected virtual void OnPrepareRepetition() { }
    }
}