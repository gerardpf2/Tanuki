using System;
using Infrastructure.Tweening.EasingFunctions;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.Tweening.Builders
{
    public class TweenBuilder<TTarget, T> : TweenBaseBuilderHelper<ITweenBuilder<TTarget, T>, ITween<TTarget, T>>, ITweenBuilder<TTarget, T>
    {
        [NotNull] private readonly Action<TTarget, T> _setter;
        [NotNull] private readonly IEasingFunctionGetter _easingFunctionGetter;
        [NotNull] private readonly Func<T, T, float, T> _lerp;

        public TTarget Target { get; private set; }

        public T Start { get; private set; }

        public T End { get; private set; }

        public float DurationS { get; private set; }

        public EasingType EasingType { get; private set; } = TweenBuilderConstants.EasingType;

        public bool ComplementaryEasingTypeBackwards { get; private set; }

        protected override ITweenBuilder<TTarget, T> This => this;

        public TweenBuilder(
            [NotNull] Action<TTarget, T> setter,
            [NotNull] IEasingFunctionGetter easingFunctionGetter,
            [NotNull] Func<T, T, float, T> lerp)
        {
            ArgumentNullException.ThrowIfNull(setter);
            ArgumentNullException.ThrowIfNull(easingFunctionGetter);
            ArgumentNullException.ThrowIfNull(lerp);

            _setter = setter;
            _easingFunctionGetter = easingFunctionGetter;
            _lerp = lerp;
        }

        public ITweenBuilder<TTarget, T> WithTarget(TTarget target)
        {
            Target = target;

            return This;
        }

        public ITweenBuilder<TTarget, T> WithStart(T start)
        {
            Start = start;

            return This;
        }

        public ITweenBuilder<TTarget, T> WithEnd(T end)
        {
            End = end;

            return This;
        }

        public ITweenBuilder<TTarget, T> WithDurationS(float durationS)
        {
            DurationS = durationS;

            return This;
        }

        public ITweenBuilder<TTarget, T> WithEasingType(EasingType easingType)
        {
            EasingType = easingType;

            return This;
        }

        public ITweenBuilder<TTarget, T> WithComplementaryEasingTypeBackwards(bool complementaryEasingTypeBackwards)
        {
            ComplementaryEasingTypeBackwards = complementaryEasingTypeBackwards;

            return This;
        }

        protected override ITween<TTarget, T> BuildTween()
        {
            IEasingFunction easingFunction = _easingFunctionGetter.Get(EasingType);
            IEasingFunction easingFunctionBackwards = ComplementaryEasingTypeBackwards ?
                _easingFunctionGetter.GetComplementary(EasingType) :
                easingFunction;

            return
                new Tween<TTarget, T>(
                    AutoPlay,
                    DelayBeforeS,
                    DelayAfterS,
                    Repetitions,
                    RepetitionType,
                    DelayManagementRepetition,
                    DelayManagementRestart,
                    OnStep,
                    OnStartIteration,
                    OnStartPlay,
                    OnPlay,
                    OnEndPlay,
                    OnEndIteration,
                    OnComplete,
                    OnPause,
                    OnResume,
                    OnRestart,
                    Target,
                    Start,
                    End,
                    DurationS,
                    _setter,
                    easingFunction,
                    easingFunctionBackwards,
                    _lerp
                );
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is not TweenBuilder<TTarget, T> other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_setter, _easingFunctionGetter, _lerp);
        }

        private bool Equals([NotNull] TweenBuilder<TTarget, T> other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return
                Equals(_setter, other._setter) &&
                Equals(_easingFunctionGetter, other._easingFunctionGetter) &&
                Equals(_lerp, other._lerp);
        }
    }
}