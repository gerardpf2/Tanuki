using System;
using Infrastructure.System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Tweening
{
    public class Tween<T> : TweenBase
    {
        private readonly T _start;
        private readonly T _end;
        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)] private readonly float _durationS;
        [NotNull] private readonly Action<T> _setter;
        [NotNull] private readonly Func<float, float> _ease;
        [NotNull] private readonly Func<T, T, float, T> _lerp;

        private float _playTimeS;

        public Tween(
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
            Action onComplete,
            T start,
            T end,
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)] float durationS,
            [NotNull] Action<T> setter,
            [NotNull] Func<float, float> ease,
            [NotNull] Func<T, T, float, T> lerp) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onStartIteration, onStartPlay, onEndPlay, onEndIteration, onPause, onResume, onRestart, onComplete)
        {
            ArgumentOutOfRangeException.ThrowIfNot(durationS, ComparisonOperator.GreaterThanOrEqualTo, 0.0f);
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

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f), Is(ComparisonOperator.LessThanOrEqualTo, "deltaTimeS")]
        protected override float Play([Is(ComparisonOperator.GreaterThan, 0.0f)] float deltaTimeS, bool backwards)
        {
            ArgumentOutOfRangeException.ThrowIfNot(deltaTimeS, ComparisonOperator.GreaterThan, 0.0f);

            backwards ^= Backwards;

            _playTimeS += deltaTimeS;

            if (_playTimeS < _durationS)
            {
                float normalizedTime = _playTimeS / _durationS;

                _setter(_lerp(GetStart(backwards), GetEnd(backwards), _ease(normalizedTime)));

                return 0.0f;
            }

            _setter(GetEnd(backwards));

            float remainingDeltaTimeS = _playTimeS - _durationS;

            if (remainingDeltaTimeS > deltaTimeS)
            {
                InvalidOperationException.Throw("Remaining delta time cannot be greater than delta time");
            }

            return remainingDeltaTimeS;
        }

        protected override void OnRestart()
        {
            base.OnRestart();

            _playTimeS = 0.0f;
        }

        protected override void OnPrepareRepetition()
        {
            base.OnPrepareRepetition();

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