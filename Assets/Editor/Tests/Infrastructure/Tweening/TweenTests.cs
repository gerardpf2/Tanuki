using System;
using Infrastructure.Tweening;
using Infrastructure.Tweening.EasingFunctions;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class TweenTests
    {
        // TODO

        private Action _onStartIteration;
        private Action _onStartPlay;
        private Action _onEndPlay;
        private Action _onEndIteration;
        private Action _onPause;
        private Action _onResume;
        private Action _onRestart;
        private Action _onComplete;
        private object _start;
        private object _end;
        private Action<object> _setter;
        private IEasingFunction _easingFunction;
        private IEasingFunction _easingFunctionBackwards;
        private Func<object, object, float, object> _lerp;

        [SetUp]
        public void SetUp()
        {
            _onStartIteration = Substitute.For<Action>();
            _onStartPlay = Substitute.For<Action>();
            _onEndPlay = Substitute.For<Action>();
            _onEndIteration = Substitute.For<Action>();
            _onPause = Substitute.For<Action>();
            _onResume = Substitute.For<Action>();
            _onRestart = Substitute.For<Action>();
            _onComplete = Substitute.For<Action>();
            _start = new object();
            _end = new object();
            _setter = Substitute.For<Action<object>>();
            _easingFunction = Substitute.For<IEasingFunction>();
            _easingFunctionBackwards = Substitute.For<IEasingFunction>();
            _lerp = Substitute.For<Func<object, object, float, object>>();
        }

        private ITween Build(
            bool autoPlay = true,
            float delayBeforeS = 0.0f,
            float delayAfterS = 0.0f,
            int repetitions = 0,
            RepetitionType repetitionType = RepetitionType.Restart,
            DelayManagement delayManagementRepetition = DelayManagement.BeforeAndAfter,
            DelayManagement delayManagementRestart = DelayManagement.BeforeAndAfter,
            float durationS = 1.0f)
        {
            return
                new Tween<object>(
                    autoPlay,
                    delayBeforeS,
                    delayAfterS,
                    repetitions,
                    repetitionType,
                    delayManagementRepetition,
                    delayManagementRestart,
                    _onStartIteration,
                    _onStartPlay,
                    _onEndPlay,
                    _onEndIteration,
                    _onPause,
                    _onResume,
                    _onRestart,
                    _onComplete,
                    _start,
                    _end,
                    durationS,
                    _setter,
                    _easingFunction,
                    _easingFunctionBackwards,
                    _lerp
                );
        }
    }
}