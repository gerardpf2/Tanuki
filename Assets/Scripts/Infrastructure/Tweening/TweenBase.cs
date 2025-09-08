using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
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
        private float _waitTimeS;
        private int _iteration;
        private bool _paused;

        public TweenState State
        {
            get => _state;
            private set
            {
                if (State == value)
                {
                    InvalidOperationException.Throw($"State is already {value}");
                }

                _state = value;

                CheckCallbackOnStateUpdated();
            }
        }

        public bool Paused
        {
            get => _paused;
            private set
            {
                if (Paused == value)
                {
                    return;
                }

                _paused = value;

                if (Paused)
                {
                    _onPause?.Invoke();
                }
                else
                {
                    _onResume?.Invoke();
                }
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

        public float Step(float deltaTimeS, bool backwards = false)
        {
            if (Paused)
            {
                return 0.0f;
            }

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
                case TweenState.PrepareRepetition:
                    ProcessPrepareRepetition();
                    break;
                case TweenState.Complete:
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(State);
                    return 0.0f;
            }

            return deltaTimeS;
        }

        public void Pause()
        {
            Paused = true;
        }

        public void Resume()
        {
            Paused = false;
        }

        public void Restart()
        {
            State = TweenState.StartIteration;

            Backwards = false;

            _delayManagement = _delayManagementRestart;
            _waitTimeS = 0.0f;
            _iteration = 0;

            _onRestart?.Invoke();

            OnRestart();
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
                case TweenState.PrepareRepetition:
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
            State = TweenState.StartIteration;

            if (!_autoPlay)
            {
                Pause();
            }
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
                State = TweenState.PrepareRepetition;
            }
            else
            {
                State = TweenState.Complete;
            }
        }

        private void ProcessPrepareRepetition()
        {
            State = TweenState.StartIteration;

            if (_repetitionType is RepetitionType.Yoyo)
            {
                Backwards = !Backwards;
            }

            _delayManagement = _delayManagementRepetition;

            OnPrepareRepetition();
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

            if (remainingDeltaTimeS > deltaTimeS)
            {
                InvalidOperationException.Throw("Remaining delta time cannot be greater than delta time");
            }

            return remainingDeltaTimeS;
        }

        protected abstract float Play(float deltaTimeS, bool backwards);

        protected virtual void OnPrepareRepetition() { }

        protected virtual void OnRestart() { }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is not TweenBase other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new();

            hashCode.Add(_autoPlay);
            hashCode.Add(_delayBeforeS);
            hashCode.Add(_delayAfterS);
            hashCode.Add(_repetitions);
            hashCode.Add(_repetitionType);
            hashCode.Add(_delayManagementRepetition);
            hashCode.Add(_delayManagementRestart);
            hashCode.Add(_onStartIteration);
            hashCode.Add(_onStartPlay);
            hashCode.Add(_onEndPlay);
            hashCode.Add(_onEndIteration);
            hashCode.Add(_onPause);
            hashCode.Add(_onResume);
            hashCode.Add(_onRestart);
            hashCode.Add(_onComplete);

            return hashCode.ToHashCode();
        }

        private bool Equals([NotNull] TweenBase other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return
                _autoPlay.Equals(other._autoPlay) &&
                _delayBeforeS.Equals(other._delayBeforeS) &&
                _delayAfterS.Equals(other._delayAfterS) &&
                _repetitions.Equals(other._repetitions) &&
                EqualityComparer<RepetitionType>.Default.Equals(_repetitionType, other._repetitionType) &&
                EqualityComparer<DelayManagement>.Default.Equals(_delayManagementRepetition, other._delayManagementRepetition) &&
                EqualityComparer<DelayManagement>.Default.Equals(_delayManagementRestart, other._delayManagementRestart) &&
                Equals(_onStartIteration, other._onStartIteration) &&
                Equals(_onStartPlay, other._onStartPlay) &&
                Equals(_onEndPlay, other._onEndPlay) &&
                Equals(_onEndIteration, other._onEndIteration) &&
                Equals(_onPause, other._onPause) &&
                Equals(_onResume, other._onResume) &&
                Equals(_onRestart, other._onRestart) &&
                Equals(_onComplete, other._onComplete);
        }
    }
}