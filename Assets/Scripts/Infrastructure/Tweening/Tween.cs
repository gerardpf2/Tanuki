using System;
using System.Collections.Generic;
using Infrastructure.Tweening.EasingFunctions;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Tweening
{
    public class Tween<T> : TweenBase
    {
        private readonly T _start;
        private readonly T _end;
        private readonly float _durationS;
        [NotNull] private readonly Action<T> _setter;
        [NotNull] private readonly IEasingFunction _easingFunction;
        [NotNull] private readonly IEasingFunction _easingFunctionBackwards;
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
            Action onStep,
            Action onStartIteration,
            Action onStartPlay,
            Action onPlay,
            Action onEndPlay,
            Action onEndIteration,
            Action onComplete,
            Action onPause,
            Action onResume,
            Action onRestart,
            T start,
            T end,
            float durationS,
            [NotNull] Action<T> setter,
            [NotNull] IEasingFunction easingFunction,
            [NotNull] IEasingFunction easingFunctionBackwards,
            [NotNull] Func<T, T, float, T> lerp) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onStep, onStartIteration, onStartPlay, onPlay, onEndPlay, onEndIteration, onComplete, onPause, onResume, onRestart)
        {
            ArgumentNullException.ThrowIfNull(setter);
            ArgumentNullException.ThrowIfNull(easingFunction);
            ArgumentNullException.ThrowIfNull(easingFunctionBackwards);
            ArgumentNullException.ThrowIfNull(lerp);

            _start = start;
            _end = end;
            _durationS = durationS;
            _setter = setter;
            _easingFunction = easingFunction;
            _easingFunctionBackwards = easingFunctionBackwards;
            _lerp = lerp;
        }

        protected override float Play(float deltaTimeS, bool backwards)
        {
            backwards ^= Backwards;

            _playTimeS += deltaTimeS;

            if (_playTimeS < _durationS)
            {
                float normalizedTime = _playTimeS / _durationS;

                _setter(
                    _lerp(
                        GetStart(backwards),
                        GetEnd(backwards),
                        backwards ?
                            _easingFunctionBackwards.Evaluate(normalizedTime) :
                            _easingFunction.Evaluate(normalizedTime)
                    )
                );

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

        protected override void OnPrepareRepetition()
        {
            base.OnPrepareRepetition();

            _playTimeS = 0.0f;
        }

        protected override void OnRestart()
        {
            base.OnRestart();

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

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) // Already checks null / ReferenceEquals
            {
                return false;
            }

            if (obj is not Tween<T> other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return
                HashCode.Combine(
                    base.GetHashCode(),
                    _start,
                    _end,
                    _durationS,
                    _setter,
                    _easingFunction,
                    _easingFunctionBackwards,
                    _lerp
                );
        }

        private bool Equals([NotNull] Tween<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return
                EqualityComparer<T>.Default.Equals(_start, other._start) &&
                EqualityComparer<T>.Default.Equals(_end, other._end) &&
                _durationS.Equals(other._durationS) &&
                Equals(_setter, other._setter) &&
                Equals(_easingFunction, other._easingFunction) &&
                Equals(_easingFunctionBackwards, other._easingFunctionBackwards) &&
                Equals(_lerp, other._lerp);
        }
    }
}