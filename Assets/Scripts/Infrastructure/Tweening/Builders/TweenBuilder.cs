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
        [NotNull] private readonly IEasingFunctionGetter _easingFunctionGetter;
        [NotNull] private readonly Func<T, T, float, T> _lerp;

        private T _start;
        private T _end;
        private float _durationS;
        private Action<T> _setter;
        private EasingType _easingType = TweenBuilderConstants.EasingType;
        private bool _complementaryEasingTypeBackwards;

        protected override ITweenBuilder<T> This => this;

        public TweenBuilder(
            [NotNull] IEasingFunctionGetter easingFunctionGetter,
            [NotNull] Func<T, T, float, T> lerp)
        {
            ArgumentNullException.ThrowIfNull(easingFunctionGetter);
            ArgumentNullException.ThrowIfNull(lerp);

            _easingFunctionGetter = easingFunctionGetter;
            _lerp = lerp;
        }

        public ITweenBuilder<T> WithStart(T start)
        {
            _start = start;

            return This;
        }

        public ITweenBuilder<T> WithEnd(T end)
        {
            _end = end;

            return This;
        }

        public ITweenBuilder<T> WithDurationS([Is(ComparisonOperator.GreaterThanOrEqualTo, 0.0f)] float durationS)
        {
            ArgumentOutOfRangeException.ThrowIfNot(durationS, ComparisonOperator.GreaterThanOrEqualTo, 0.0f);

            _durationS = durationS;

            return This;
        }

        public ITweenBuilder<T> WithSetter([NotNull] Action<T> setter)
        {
            ArgumentNullException.ThrowIfNull(setter);

            _setter = setter;

            return This;
        }

        public ITweenBuilder<T> WithEasingType(EasingType easingType)
        {
            _easingType = easingType;

            return This;
        }

        public ITweenBuilder<T> WithComplementaryEasingTypeBackwards(bool complementaryEasingTypeBackwards)
        {
            _complementaryEasingTypeBackwards = complementaryEasingTypeBackwards;

            return This;
        }

        public override ITween Build()
        {
            InvalidOperationException.ThrowIfNot(_durationS, ComparisonOperator.GreaterThanOrEqualTo, 0.0f);
            InvalidOperationException.ThrowIfNull(_setter);

            IEasingFunction easingFunction = _easingFunctionGetter.Get(_easingType);
            IEasingFunction easingFunctionBackwards = _complementaryEasingTypeBackwards ?
                _easingFunctionGetter.GetComplementary(_easingType) :
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
                    _start,
                    _end,
                    _durationS,
                    _setter,
                    easingFunction,
                    easingFunctionBackwards,
                    _lerp
                );
        }
    }
}