using System;
using JetBrains.Annotations;
using UnityEngine.UIElements;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Tweening.TweenBuilders
{
    public abstract class TweenBuilder<T> : ITweenBuilder<T>
    {
        [NotNull] private readonly IEasingFunctionGetter _easingFunctionGetter;
        [NotNull] private readonly Func<T, T, float, T> _lerp;

        private T _start;
        private T _end;
        private float _delayS;
        private float _durationS;
        private Action _onComplete;
        private Action<T> _setter;
        private EasingMode _easingMode;

        protected TweenBuilder(
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

            return this;
        }

        public ITweenBuilder<T> WithEnd(T end)
        {
            _end = end;

            return this;
        }

        public ITweenBuilder<T> WithDelayS(float delayS)
        {
            _delayS = delayS;

            return this;
        }

        public ITweenBuilder<T> WithDurationS(float durationS)
        {
            _durationS = durationS;

            return this;
        }

        public ITweenBuilder<T> WithOnComplete(Action onComplete)
        {
            _onComplete = onComplete;

            return this;
        }

        public ITweenBuilder<T> WithSetter([NotNull] Action<T> setter)
        {
            ArgumentNullException.ThrowIfNull(setter);

            _setter = setter;

            return this;
        }

        public ITweenBuilder<T> WithEasingMode(EasingMode easingMode)
        {
            _easingMode = easingMode;

            return this;
        }

        public ITween Build()
        {
            InvalidOperationException.ThrowIfNull(_setter);

            return
                new Tween<T>(
                    _start,
                    _end,
                    _delayS,
                    _durationS,
                    _onComplete,
                    _setter,
                    _easingFunctionGetter.Get(_easingMode),
                    _lerp
                );
        }
    }
}