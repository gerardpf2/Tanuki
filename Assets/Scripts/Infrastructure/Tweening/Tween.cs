using System;
using System.Collections.Generic;
using Infrastructure.Tweening.EasingFunctions;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Tweening
{
    public class Tween<TTarget, T> : TweenBase<ITween<TTarget, T>>, ITween<TTarget, T>
    {
        private readonly TTarget _target;
        private readonly T _start;
        private readonly T _end;
        private readonly float _durationS;
        [NotNull] private readonly Action<TTarget, T> _setter;
        [NotNull] private readonly IEasingFunction _easingFunction;
        [NotNull] private readonly IEasingFunction _easingFunctionBackwards;
        [NotNull] private readonly Func<T, T, float, T> _lerp;

        private float _playTimeS;

        protected override ITween<TTarget, T> This => this;

        public Tween(
            bool autoPlay,
            float delayBeforeS,
            float delayAfterS,
            int repetitions,
            RepetitionType repetitionType,
            DelayManagement delayManagementRepetition,
            DelayManagement delayManagementRestart,
            Action<ITween<TTarget, T>> onStep,
            Action<ITween<TTarget, T>> onStartIteration,
            Action<ITween<TTarget, T>> onStartPlay,
            Action<ITween<TTarget, T>> onPlay,
            Action<ITween<TTarget, T>> onEndPlay,
            Action<ITween<TTarget, T>> onEndIteration,
            Action<ITween<TTarget, T>> onComplete,
            Action<ITween<TTarget, T>> onPause,
            Action<ITween<TTarget, T>> onResume,
            Action<ITween<TTarget, T>> onRestart,
            TTarget target,
            T start,
            T end,
            float durationS,
            [NotNull] Action<TTarget, T> setter,
            [NotNull] IEasingFunction easingFunction,
            [NotNull] IEasingFunction easingFunctionBackwards,
            [NotNull] Func<T, T, float, T> lerp) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onStep, onStartIteration, onStartPlay, onPlay, onEndPlay, onEndIteration, onComplete, onPause, onResume, onRestart)
        {
            ArgumentNullException.ThrowIfNull(setter);
            ArgumentNullException.ThrowIfNull(easingFunction);
            ArgumentNullException.ThrowIfNull(easingFunctionBackwards);
            ArgumentNullException.ThrowIfNull(lerp);

            _target = target;
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
                    _target,
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

            _setter(_target, GetEnd(backwards));

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

            if (obj is not Tween<TTarget, T> other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new();

            hashCode.Add(base.GetHashCode());
            hashCode.Add(_target);
            hashCode.Add(_start);
            hashCode.Add(_end);
            hashCode.Add(_durationS);
            hashCode.Add(_setter);
            hashCode.Add(_easingFunction);
            hashCode.Add(_easingFunctionBackwards);
            hashCode.Add(_lerp);

            return hashCode.ToHashCode();
        }

        private bool Equals([NotNull] Tween<TTarget, T> other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return
                EqualityComparer<TTarget>.Default.Equals(_target, other._target) &&
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