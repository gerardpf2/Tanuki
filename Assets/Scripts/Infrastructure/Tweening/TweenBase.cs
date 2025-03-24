using System;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Infrastructure.Tweening
{
    public abstract class TweenBase : ITween
    {
        private readonly float _delayS;
        private readonly int _repetitions;
        private readonly RepetitionType _repetitionType;
        private readonly Action _onIterationComplete;
        private readonly Action _onComplete;

        private TweenState _tweenState;
        private float _playingTimeS;
        private int _iteration;

        protected TweenBase(
            float delayS,
            int repetitions,
            RepetitionType repetitionType,
            Action onIterationComplete,
            Action onComplete)
        {
            _delayS = delayS;
            _repetitions = repetitions;
            _repetitionType = repetitionType;
            _onIterationComplete = onIterationComplete;
            _onComplete = onComplete;
        }

        public TweenState Update(float deltaTimeS)
        {
            if (_tweenState != TweenState.Playing)
            {
                return _tweenState;
            }

            _playingTimeS += deltaTimeS;

            float sinceDelayS = _playingTimeS - _delayS;

            if (sinceDelayS < 0.0f)
            {
                return _tweenState;
            }

            if (CanRefresh(sinceDelayS))
            {
                Refresh(deltaTimeS, sinceDelayS);
            }
            else
            {
                Complete();
            }

            return _tweenState;
        }

        public bool Pause()
        {
            if (_tweenState != TweenState.Playing)
            {
                return false;
            }

            _tweenState = TweenState.Paused;

            return true;
        }

        public bool Resume()
        {
            if (_tweenState != TweenState.Paused)
            {
                return false;
            }

            _tweenState = TweenState.Playing;

            return true;
        }

        protected abstract bool CanRefresh(float sinceDelayS);

        protected abstract void Refresh(float deltaTimeS, float sinceDelayS);

        private void Complete()
        {
            OnComplete();

            ++_iteration;

            _onIterationComplete?.Invoke();

            if (_repetitions < 0 || _iteration <= _repetitions)
            {
                PrepareNextRepetition();
            }
            else
            {
                _tweenState = TweenState.Completed;

                _onComplete?.Invoke();
            }
        }

        protected virtual void OnComplete() { }

        private void PrepareNextRepetition()
        {
            switch (_repetitionType)
            {
                case RepetitionType.Restart:
                    ResetPlayingTime(false);
                    break;
                case RepetitionType.RestartWithDelayS:
                    ResetPlayingTime(true);
                    break;
                case RepetitionType.Yoyo:
                    ResetPlayingTime(false);
                    ApplyYoyo();
                    break;
                case RepetitionType.YoyoWithDelayS:
                    ResetPlayingTime(true);
                    ApplyYoyo();
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(_repetitionType);
                    break;
            }
        }

        private void ResetPlayingTime(bool withDelay)
        {
            _playingTimeS = withDelay ? 0.0f : _delayS;
        }

        protected abstract void ApplyYoyo();
    }
}