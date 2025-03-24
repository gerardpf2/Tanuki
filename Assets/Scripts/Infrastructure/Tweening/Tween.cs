using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

namespace Infrastructure.Tweening
{
    public class Tween<T> : ITween
    {
        private T _start;
        private T _end;
        private readonly float _delayS;
        private readonly float _durationS;
        private readonly int _repetitions;
        private readonly RepetitionType _repetitionType;
        private readonly Action _onIterationComplete;
        private readonly Action _onComplete;
        [NotNull] private readonly Action<T> _setter;
        [NotNull] private readonly Func<float, float> _ease;
        [NotNull] private readonly Func<T, T, float, T> _lerp;

        private TweenState _tweenState;
        private float _playingTimeS;
        private int _iteration;

        public Tween(
            T start,
            T end,
            float delayS,
            float durationS,
            int repetitions,
            RepetitionType repetitionType,
            Action onIterationComplete,
            Action onComplete,
            [NotNull] Action<T> setter,
            [NotNull] Func<float, float> ease,
            [NotNull] Func<T, T, float, T> lerp)
        {
            ArgumentNullException.ThrowIfNull(setter);
            ArgumentNullException.ThrowIfNull(ease);
            ArgumentNullException.ThrowIfNull(lerp);

            _start = start;
            _end = end;
            _delayS = delayS;
            _durationS = durationS;
            _repetitions = repetitions;
            _repetitionType = repetitionType;
            _onIterationComplete = onIterationComplete;
            _onComplete = onComplete;
            _setter = setter;
            _ease = ease;
            _lerp = lerp;
        }

        public TweenState Update(float deltaTimeS)
        {
            if (_tweenState != TweenState.Playing)
            {
                return _tweenState;
            }

            _playingTimeS += deltaTimeS;

            if (_playingTimeS < _delayS)
            {
                return _tweenState;
            }

            if (_playingTimeS - _delayS < _durationS)
            {
                Update();
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

        private void Update()
        {
            float normalizedTime = (_playingTimeS - _delayS) / _durationS;

            _setter(_lerp(_start, _end, _ease(normalizedTime)));
        }

        private void Complete()
        {
            ++_iteration;

            _setter(_end);

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

        private void PrepareNextRepetition()
        {
            switch (_repetitionType)
            {
                case RepetitionType.Restart:
                    UpdatePlayingTimeS();
                    break;
                case RepetitionType.RestartWithDelayS:
                    UpdatePlayingTimeSWithDelayS();
                    break;
                case RepetitionType.Yoyo:
                    UpdatePlayingTimeS();
                    SwapStartEnd();
                    break;
                case RepetitionType.YoyoWithDelayS:
                    UpdatePlayingTimeSWithDelayS();
                    SwapStartEnd();
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(_repetitionType);
                    break;
            }

            return;

            void UpdatePlayingTimeS()
            {
                _playingTimeS -= _durationS;
            }

            void UpdatePlayingTimeSWithDelayS()
            {
                _playingTimeS = _playingTimeS - _durationS - _delayS;
            }

            void SwapStartEnd()
            {
                (_start, _end) = (_end, _start);
            }
        }
    }
}