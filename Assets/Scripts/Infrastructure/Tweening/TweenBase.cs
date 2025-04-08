using System;
using System.Collections.Generic;
using Infrastructure.System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Tweening
{
    public abstract class TweenBase : ITween
    {
        private readonly bool _autoPlay;
        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)] private readonly float _delayBeforeS;
        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)] private readonly float _delayAfterS;
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
                if (State == value)
                {
                    InvalidOperationException.Throw($"State is already {value}");
                }

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
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)] float delayBeforeS,
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)] float delayAfterS,
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
            ArgumentOutOfRangeException.ThrowIfNot(delayBeforeS, ComparisonOperator.GreaterThanOrEqualTo, 0.0f);
            ArgumentOutOfRangeException.ThrowIfNot(delayAfterS, ComparisonOperator.GreaterThanOrEqualTo, 0.0f);

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

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f), Is(ComparisonOperator.LessThanOrEqualTo, "deltaTimeS")]
        public float Update([Is(ComparisonOperator.GreaterThan, 0.0f)] float deltaTimeS, bool backwards = false)
        {
            ArgumentOutOfRangeException.ThrowIfNot(deltaTimeS, ComparisonOperator.GreaterThan, 0.0f);

            do
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
                    case TweenState.PrepareRepetition:
                        ProcessPrepareRepetition();
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
            } while (deltaTimeS > 0.0f);

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

        public bool Restart()
        {
            if (State is TweenState.Restart)
            {
                return false;
            }

            State = TweenState.Restart;

            return true;
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

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f), Is(ComparisonOperator.LessThanOrEqualTo, "deltaTimeS")]
        private float ProcessWaitBefore([Is(ComparisonOperator.GreaterThan, 0.0f)] float deltaTimeS)
        {
            ArgumentOutOfRangeException.ThrowIfNot(deltaTimeS, ComparisonOperator.GreaterThan, 0.0f);

            return ProcessWait(deltaTimeS, _delayBeforeS, TweenState.StartPlay);
        }

        private void ProcessStartPlay()
        {
            State = TweenState.Play;
        }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f), Is(ComparisonOperator.LessThanOrEqualTo, "deltaTimeS")]
        private float ProcessPlay([Is(ComparisonOperator.GreaterThan, 0.0f)] float deltaTimeS, bool backwards)
        {
            ArgumentOutOfRangeException.ThrowIfNot(deltaTimeS, ComparisonOperator.GreaterThan, 0.0f);

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

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f), Is(ComparisonOperator.LessThanOrEqualTo, "deltaTimeS")]
        private float ProcessWaitAfter([Is(ComparisonOperator.GreaterThan, 0.0f)] float deltaTimeS)
        {
            ArgumentOutOfRangeException.ThrowIfNot(deltaTimeS, ComparisonOperator.GreaterThan, 0.0f);

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

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f), Is(ComparisonOperator.LessThanOrEqualTo, "deltaTimeS")]
        private float ProcessWait(
            [Is(ComparisonOperator.GreaterThan, 0.0f)] float deltaTimeS,
            float delayS,
            TweenState nextState)
        {
            ArgumentOutOfRangeException.ThrowIfNot(deltaTimeS, ComparisonOperator.GreaterThan, 0.0f);

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

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f), Is(ComparisonOperator.LessThanOrEqualTo, "deltaTimeS")]
        protected abstract float Play([Is(ComparisonOperator.GreaterThan, 0.0f)] float deltaTimeS, bool backwards);

        protected virtual void OnRestart() { }

        protected virtual void OnPrepareRepetition() { }

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

        protected bool Equals([NotNull] TweenBase other)
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