using System;
using JetBrains.Annotations;
using UnityEngine.UIElements;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Tweening.TweenBuilders
{
    public class TweenBuilder<T> : TweenBaseBuilderHelper<ITweenBuilder<T>>, ITweenBuilder<T>
    {
        [NotNull] private readonly IEasingFunctionGetter _easingFunctionGetter;
        [NotNull] private readonly Func<T, T, float, T> _lerp;

        private T _start;
        private T _end;
        private float _durationS;
        private Action<T> _setter;
        private EasingMode _easingMode;

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

        public ITweenBuilder<T> WithDurationS(float durationS)
        {
            _durationS = durationS;

            return This;
        }

        public ITweenBuilder<T> WithSetter([NotNull] Action<T> setter)
        {
            ArgumentNullException.ThrowIfNull(setter);

            _setter = setter;

            return This;
        }

        public ITweenBuilder<T> WithEasingMode(EasingMode easingMode)
        {
            _easingMode = easingMode;

            return This;
        }

        protected override void CustomReset()
        {
            base.CustomReset();

            _easingMode = EasingMode.EaseOut;
        }

        protected override ITween BuildTween()
        {
            InvalidOperationException.ThrowIfNull(_setter);

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
                    OnPaused,
                    OnCompleted,
                    _start,
                    _end,
                    _durationS,
                    _setter,
                    _easingFunctionGetter.Get(_easingMode),
                    _lerp
                );
        }
    }
}