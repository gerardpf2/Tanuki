using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening
{
    public class Tween<T> : ITween
    {
        private readonly T _start;
        private readonly T _end;
        private readonly float _durationS;
        [NotNull] private readonly Action<T> _setter;
        [NotNull] private readonly Func<float, float> _ease;
        [NotNull] private readonly Func<T, T, float, T> _lerp;

        private TweenState _tweenState;
        private float _playingTimeS;

        public Tween(
            T start,
            T end,
            float durationS,
            [NotNull] Action<T> setter,
            [NotNull] Func<float, float> ease,
            [NotNull] Func<T, T, float, T> lerp)
        {
            ArgumentNullException.ThrowIfNull(setter);
            ArgumentNullException.ThrowIfNull(ease);
            ArgumentNullException.ThrowIfNull(lerp);

            _start = start;
            _end = end;
            _durationS = durationS;
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

            if (_playingTimeS < _durationS)
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
            float normalizedTime = _playingTimeS / _durationS;

            _setter(_lerp(_start, _end, _ease(normalizedTime)));
        }

        private void Complete()
        {
            _tweenState = TweenState.Completed;

            _setter(_end);
        }
    }
}