using System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening
{
    public class Tween<T> : TweenBase
    {
        private readonly T _start;
        private readonly T _end;
        private readonly float _durationS;
        [NotNull] private readonly Action<T> _setter;
        [NotNull] private readonly Func<float, float> _ease;
        [NotNull] private readonly Func<T, T, float, T> _lerp;

        private float _playingTimeS;

        public Tween(
            float delayS,
            bool autoPlay,
            int repetitions,
            RepetitionType repetitionType,
            Action onIterationComplete,
            Action onComplete,
            T start,
            T end,
            float durationS,
            [NotNull] Action<T> setter,
            [NotNull] Func<float, float> ease,
            [NotNull] Func<T, T, float, T> lerp) : base(delayS, autoPlay, repetitions, repetitionType, onIterationComplete, onComplete)
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

        public override void Restart(bool withDelay)
        {
            base.Restart(withDelay);

            _playingTimeS = 0.0f;
        }

        protected override float Refresh(float deltaTimeS, bool backwards)
        {
            backwards ^= Backwards;

            _playingTimeS += deltaTimeS;

            if (_playingTimeS < _durationS)
            {
                float normalizedTime = _playingTimeS / _durationS;

                _setter(_lerp(GetStart(backwards), GetEnd(backwards), _ease(normalizedTime)));

                return 0.0f;
            }

            _setter(GetEnd(backwards));

            return _playingTimeS - _durationS;
        }

        protected override void RestartForNextIteration(bool withDelay)
        {
            base.RestartForNextIteration(withDelay);

            _playingTimeS = 0.0f;
        }

        private T GetStart(bool backwards)
        {
            return backwards ? _end : _start;
        }

        private T GetEnd(bool backwards)
        {
            return backwards ? _start : _end;
        }
    }
}