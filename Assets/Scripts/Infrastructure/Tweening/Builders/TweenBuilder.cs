using System;
using Infrastructure.System;
using Infrastructure.Tweening.EasingFunctions;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Tweening.Builders
{
    public class TweenBuilder<T> : TweenBaseBuilderHelper<ITweenBuilder<T>>, ITweenBuilder<T>
    {
        [NotNull] private readonly Action<T> _setter;
        [NotNull] private readonly IEasingFunctionGetter _easingFunctionGetter;
        [NotNull] private readonly Func<T, T, float, T> _lerp;

        private float _durationS;

        public T Start { get; private set; }

        public T End { get; private set; }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)]
        public float DurationS
        {
            get
            {
                InvalidOperationException.ThrowIfNot(_durationS, ComparisonOperator.GreaterThanOrEqualTo, 0.0f);

                return _durationS;
            }
            private set => _durationS = value;
        }

        public EasingType EasingType { get; private set; } = TweenBuilderConstants.EasingType;

        public bool ComplementaryEasingTypeBackwards { get; private set; }

        protected override ITweenBuilder<T> This => this;

        public TweenBuilder(
            [NotNull] Action<T> setter,
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

        public ITweenBuilder<T> WithStart(T start)
        {
            Start = start;

            return This;
        }

        public ITweenBuilder<T> WithEnd(T end)
        {
            End = end;

            return This;
        }

        public ITweenBuilder<T> WithDurationS([Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)] float durationS)
        {
            ArgumentOutOfRangeException.ThrowIfNot(durationS, ComparisonOperator.GreaterThanOrEqualTo, 0.0f);

            DurationS = durationS;

            return This;
        }

        public ITweenBuilder<T> WithEasingType(EasingType easingType)
        {
            EasingType = easingType;

            return This;
        }

        public ITweenBuilder<T> WithComplementaryEasingTypeBackwards(bool complementaryEasingTypeBackwards)
        {
            ComplementaryEasingTypeBackwards = complementaryEasingTypeBackwards;

            return This;
        }

        protected override ITween BuildTween()
        {
            IEasingFunction easingFunction = _easingFunctionGetter.Get(EasingType);
            IEasingFunction easingFunctionBackwards = ComplementaryEasingTypeBackwards ?
                _easingFunctionGetter.GetComplementary(EasingType) :
                easingFunction;

            return
                new Tween<T>(
                    AutoPlay,
                    DelayBeforeS,
                    DelayAfterS,
                    Repetitions,
                    RepetitionType,
                    DelayManagementRepetition,
                    DelayManagementRestart,
                    OnStartIteration,
                    OnStartPlay,
                    OnEndPlay,
                    OnEndIteration,
                    OnPause,
                    OnResume,
                    OnRestart,
                    OnComplete,
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

            if (obj is not TweenBuilder<T> other)
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_setter, _easingFunctionGetter, _lerp);
        }

        private bool Equals([NotNull] TweenBuilder<T> other)
        {
            ArgumentNullException.ThrowIfNull(other);

            return
                Equals(_setter, other._setter) &&
                Equals(_easingFunctionGetter, other._easingFunctionGetter) &&
                Equals(_lerp, other._lerp);
        }
    }
}