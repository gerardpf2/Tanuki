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

        private float _playingTimeS;
        private bool _backwards;
        private int _iteration;

        public TweenState State { get; private set; }

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

            State = GetInitialState();
        }

        public void Update(float deltaTimeS, bool backwards = false)
        {
            if (State != TweenState.Playing)
            {
                return;
            }

            _playingTimeS += deltaTimeS;

            float sinceDelayS = _playingTimeS - _delayS;

            if (sinceDelayS < 0.0f)
            {
                return;
            }

            backwards ^= _backwards;

            if (CanRefresh(sinceDelayS))
            {
                Refresh(deltaTimeS, sinceDelayS, backwards);
            }
            else
            {
                Complete(backwards);
            }
        }

        public bool Play()
        {
            if (State != TweenState.Paused)
            {
                return false;
            }

            State = TweenState.Playing;

            return true;
        }

        public bool Pause()
        {
            if (State != TweenState.Playing)
            {
                return false;
            }

            State = TweenState.Paused;

            return true;
        }

        public virtual void Restart(bool withDelay)
        {
            State = GetInitialState();

            RestartPlayingTime(withDelay);

            _backwards = false;
            _iteration = 0;
        }

        private TweenState GetInitialState()
        {
            return _autoPlay ? TweenState.Playing : TweenState.Paused;
        }

        protected abstract bool CanRefresh(float sinceDelayS);

        protected abstract void Refresh(float deltaTimeS, float sinceDelayS, bool backwards);

        private void Complete(bool backwards)
        {
            ++_iteration;

            OnIterationComplete(backwards);

            _onIterationComplete?.Invoke();

            if (_repetitions < 0 || _iteration <= _repetitions)
            {
                PrepareNextRepetition();
            }
            else
            {
                State = TweenState.Completed;

                _onComplete?.Invoke();
            }
        }

        protected virtual void OnIterationComplete(bool backwards) { }

        private void PrepareNextRepetition()
        {
            switch (_repetitionType)
            {
                case RepetitionType.Restart:
                    RestartForNextRepetition(false);
                    break;
                case RepetitionType.RestartWithDelay:
                    RestartForNextRepetition(true);
                    break;
                case RepetitionType.Yoyo:
                    RestartForNextRepetition(false);
                    ApplyYoyo();
                    break;
                case RepetitionType.YoyoWithDelay:
                    RestartForNextRepetition(true);
                    ApplyYoyo();
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(_repetitionType);
                    break;
            }
        }

        protected virtual void RestartForNextRepetition(bool withDelay)
        {
            RestartPlayingTime(withDelay);
        }

        private void RestartPlayingTime(bool withDelay)
        {
            _playingTimeS = withDelay ? 0.0f : _delayS;
        }

        private void ApplyYoyo()
        {
            _backwards = !_backwards;
        }
    }
}