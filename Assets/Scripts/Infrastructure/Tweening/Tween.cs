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

        protected override bool CanRefresh(float playingTimeS)
        {
            return playingTimeS < _durationS;
        }

        protected override void Refresh(float _, float playingTimeS, bool backwards)
        {
            float normalizedTime = playingTimeS / _durationS;

            _setter(_lerp(GetStart(backwards), GetEnd(backwards), _ease(normalizedTime)));
        }

        protected override void CompleteIteration(bool backwards)
        {
            base.CompleteIteration(backwards);

            _setter(GetEnd(backwards));
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