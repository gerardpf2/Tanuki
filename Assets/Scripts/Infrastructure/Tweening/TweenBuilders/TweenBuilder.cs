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

        private bool _autoPlay;
        private float _delayBeforeS;
        private float _delayAfterS;
        private int _repetitions;
        private RepetitionType _repetitionType;
        private DelayManagement _delayManagementRepetition;
        private DelayManagement _delayManagementRestart;
        private Action _onStartIteration;
        private Action _onStartPlay;
        private Action _onEndPlay;
        private Action _onEndIteration;
        private Action _onPaused;
        private Action _onCompleted;
        private T _start;
        private T _end;
        private float _durationS;
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

            Reset();
        }

        public ITweenBuilder<T> WithAutoPlay(bool autoPlay)
        {
            _autoPlay = autoPlay;

            return this;
        }

        public ITweenBuilder<T> WithDelayBeforeS(float delayBeforeS)
        {
            _delayBeforeS = delayBeforeS;

            return this;
        }

        public ITweenBuilder<T> WithDelayAfterS(float delayAfterS)
        {
            _delayAfterS = delayAfterS;

            return this;
        }

        public ITweenBuilder<T> WithRepetitions(int repetitions)
        {
            _repetitions = repetitions;

            return this;
        }

        public ITweenBuilder<T> WithRepetitionType(RepetitionType repetitionType)
        {
            _repetitionType = repetitionType;

            return this;
        }

        public ITweenBuilder<T> WithDelayManagementRepetition(DelayManagement delayManagementRepetition)
        {
            _delayManagementRepetition = delayManagementRepetition;

            return this;
        }

        public ITweenBuilder<T> WithDelayManagementRestart(DelayManagement delayManagementRestart)
        {
            _delayManagementRestart = delayManagementRestart;

            return this;
        }

        public ITweenBuilder<T> WithOnStartIteration(Action onStartIteration)
        {
            _onStartIteration = onStartIteration;

            return this;
        }

        public ITweenBuilder<T> WithOnStartPlay(Action onStartPlay)
        {
            _onStartPlay = onStartPlay;

            return this;
        }

        public ITweenBuilder<T> WithOnEndPlay(Action onEndPlay)
        {
            _onEndPlay = onEndPlay;

            return this;
        }

        public ITweenBuilder<T> WithOnEndIteration(Action onEndIteration)
        {
            _onEndIteration = onEndIteration;

            return this;
        }

        public ITweenBuilder<T> WithOnPaused(Action onPaused)
        {
            _onPaused = onPaused;

            return this;
        }

        public ITweenBuilder<T> WithOnCompleted(Action onCompleted)
        {
            _onCompleted = onCompleted;

            return this;
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

        public ITweenBuilder<T> WithDurationS(float durationS)
        {
            _durationS = durationS;

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

            ITween tween =
                new Tween<T>(
                    _autoPlay,
                    _delayBeforeS,
                    _delayAfterS,
                    _repetitions,
                    _repetitionType,
                    _delayManagementRepetition,
                    _delayManagementRestart,
                    _onStartIteration,
                    _onStartPlay,
                    _onEndPlay,
                    _onEndIteration,
                    _onPaused,
                    _onCompleted,
                    _start,
                    _end,
                    _durationS,
                    _setter,
                    _easingFunctionGetter.Get(_easingMode),
                    _lerp
                );

            Reset();

            return tween;
        }

        private void Reset()
        {
            _autoPlay = TweenBuilderDefaults.AutoPlay;
            _delayBeforeS = 0.0f;
            _delayAfterS = 0.0f;
            _repetitions = 0;
            _repetitionType = TweenBuilderDefaults.RepetitionType;
            _delayManagementRepetition = TweenBuilderDefaults.DelayManagementRepetition;
            _delayManagementRestart = TweenBuilderDefaults.DelayManagementRestart;
            _onEndIteration = null;
            _onCompleted = null;
            _start = default;
            _end = default;
            _durationS = 0.0f;
            _setter = null;
            _easingMode = TweenBuilderDefaults.EasingMode;
        }
    }
}