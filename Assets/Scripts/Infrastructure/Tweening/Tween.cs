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

        private float _playTimeS;

        public Tween(
            bool autoPlay,
            float delayBeforeS,
            float delayAfterS,
            int repetitions,
            RepetitionType repetitionType,
            DelayManagement delayManagementRepetition,
            DelayManagement delayManagementRestart,
            Action onEndIteration,
            Action onCompleted,
            T start,
            T end,
            float durationS,
            [NotNull] Action<T> setter,
            [NotNull] Func<float, float> ease,
            [NotNull] Func<T, T, float, T> lerp) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onEndIteration, onCompleted)
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

        public override void Restart()
        {
            base.Restart();

            _playTimeS = 0.0f;
        }

        protected override float Refresh(float deltaTimeS, bool backwards)
        {
            backwards ^= Backwards;

            _playTimeS += deltaTimeS;

            if (_playTimeS < _durationS)
            {
                float normalizedTime = _playTimeS / _durationS;

                _setter(_lerp(GetStart(backwards), GetEnd(backwards), _ease(normalizedTime)));

                return 0.0f;
            }

            _setter(GetEnd(backwards));

            return _playTimeS - _durationS;
        }

        protected override void PrepareRepetition()
        {
            base.PrepareRepetition();

            _playTimeS = 0.0f;
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