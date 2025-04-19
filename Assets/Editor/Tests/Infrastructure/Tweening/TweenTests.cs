using System;
using Infrastructure.Tweening;
using Infrastructure.Tweening.EasingFunctions;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class TweenTests
    {
        // It also covers TweenBase tests

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

        private Tween<object> _tween;

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

        #region TweenBase

        [Test]
        public void Step_Paused_ReturnsZero()
        {
            const bool autoPlay = false;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = 0.0f;
            ITween tween = Build(autoPlay: autoPlay);
            tween.Step(deltaTimeS); // SetUp

            float remainingDeltaTimeS = tween.Step(deltaTimeS);

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
        }

        [Test]
        public void Step_SetUpAndAutoPlayTrue_StartIterationAndCallbackAndNotPaused()
        {
            const bool autoPlay = true;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            const TweenState expectedState = TweenState.StartIteration;
            ITween tween = Build(autoPlay: autoPlay);

            float remainingDeltaTimeS = tween.Step(deltaTimeS);
            TweenState state = tween.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onStartIteration.Received(1).Invoke();
            Assert.IsFalse(tween.Paused);
        }

        [Test]
        public void Step_SetUpAndAutoPlayFalse_StartIterationAndCallbackAndPaused()
        {
            const bool autoPlay = false;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            const TweenState expectedState = TweenState.StartIteration;
            ITween tween = Build(autoPlay: autoPlay);

            float remainingDeltaTimeS = tween.Step(deltaTimeS);
            TweenState state = tween.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onStartIteration.Received(1).Invoke();
            Assert.IsTrue(tween.Paused);
        }

        [TestCase(DelayManagement.BeforeAndAfter, TweenState.WaitBefore, false)]
        [TestCase(DelayManagement.Before, TweenState.WaitBefore, false)]
        [TestCase(DelayManagement.After, TweenState.StartPlay, true)]
        [TestCase(DelayManagement.None, TweenState.StartPlay, true)]
        public void Step_StartIteration_ExpectedBothStateAndCallback(DelayManagement delayManagement, TweenState expectedState, bool onStartPlayCalled)
        {
            const bool autoPlay = false;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            ITween tween = Build(autoPlay: autoPlay, delayManagementRestart: delayManagement);
            tween.Restart(); // Apply delayManagement and move to StartIteration

            float remainingDeltaTimeS = tween.Step(deltaTimeS);
            TweenState state = tween.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            if (onStartPlayCalled)
            {
                _onStartPlay.Received(1).Invoke();
            }
            else
            {
                _onStartPlay.DidNotReceive().Invoke();
            }
        }

        [Test]
        public void Step_WaitBeforeAndTimeNotReached_WaitBeforeAndNoCallback()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 1.5f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = 0.0f;
            const TweenState expectedState = TweenState.WaitBefore;
            ITween tween = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration

            float remainingDeltaTimeS = tween.Step(deltaTimeS);
            TweenState state = tween.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onStartPlay.DidNotReceive().Invoke();
        }

        [Test]
        public void Step_WaitBeforeAndTimeReached_StartPlayAndCallback()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.75f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = 0.25f;
            const TweenState expectedState = TweenState.StartPlay;
            ITween tween = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration

            float remainingDeltaTimeS = tween.Step(deltaTimeS);
            TweenState state = tween.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onStartPlay.Received(1).Invoke();
        }

        [Test]
        public void Step_StartPlay_Play()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            const TweenState expectedState = TweenState.Play;
            ITween tween = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration
            tween.Step(deltaTimeS); // WaitBefore

            float remainingDeltaTimeS = tween.Step(deltaTimeS);
            TweenState state = tween.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
        }

        [Test]
        public void Step_PlayAndTimeNotReached_PlayAndNoCallback()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float durationS = 1.5f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = 0.0f;
            const TweenState expectedState = TweenState.Play;
            ITween tween = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, durationS: durationS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration
            tween.Step(deltaTimeS); // WaitBefore
            tween.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = tween.Step(deltaTimeS);
            TweenState state = tween.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onEndPlay.DidNotReceive().Invoke();
        }

        [Test]
        public void Step_PlayAndTimeReached_EndPlayAndCallback()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float durationS = 0.75f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = 0.25f;
            const TweenState expectedState = TweenState.EndPlay;
            ITween tween = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, durationS: durationS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration
            tween.Step(deltaTimeS); // WaitBefore
            tween.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = tween.Step(deltaTimeS);
            TweenState state = tween.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onEndPlay.Received(1).Invoke();
        }

        [TestCase(DelayManagement.BeforeAndAfter, TweenState.WaitAfter, false)]
        [TestCase(DelayManagement.Before, TweenState.EndIteration, true)]
        [TestCase(DelayManagement.After, TweenState.WaitAfter, false)]
        [TestCase(DelayManagement.None, TweenState.EndIteration, true)]
        public void Step_EndPlay_ExpectedBothStateAndCallback(DelayManagement delayManagement, TweenState expectedState, bool onEndIterationCalled)
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float durationS = 0.75f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            ITween tween = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayManagementRestart: delayManagement, durationS: durationS);
            tween.Restart(); // Apply delayManagement and move to StartIteration
            tween.Step(deltaTimeS); // StartIteration
            if (delayManagement is DelayManagement.BeforeAndAfter or DelayManagement.Before)
            {
                tween.Step(deltaTimeS); // WaitBefore
            }
            tween.Step(deltaTimeS); // StartPlay
            tween.Step(deltaTimeS); // Play

            float remainingDeltaTimeS = tween.Step(deltaTimeS);
            TweenState state = tween.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            if (onEndIterationCalled)
            {
                _onEndIteration.Received(1).Invoke();
            }
            else
            {
                _onEndIteration.DidNotReceive().Invoke();
            }
        }

        [Test]
        public void Step_WaitAfterAndTimeNotReached_WaitAfterAndNoCallback()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float delayAfterS = 1.5f;
            const float durationS = 0.75f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = 0.0f;
            const TweenState expectedState = TweenState.WaitAfter;
            ITween tween = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, durationS: durationS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration
            tween.Step(deltaTimeS); // WaitBefore
            tween.Step(deltaTimeS); // StartPlay
            tween.Step(deltaTimeS); // Play
            tween.Step(deltaTimeS); // EndPlay

            float remainingDeltaTimeS = tween.Step(deltaTimeS);
            TweenState state = tween.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onEndIteration.DidNotReceive().Invoke();
        }

        [Test]
        public void Step_WaitAfterAndTimeReached_EndIterationAndCallback()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float delayAfterS = 0.75f;
            const float durationS = 0.75f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = 0.25f;
            const TweenState expectedState = TweenState.EndIteration;
            ITween tween = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, durationS: durationS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration
            tween.Step(deltaTimeS); // WaitBefore
            tween.Step(deltaTimeS); // StartPlay
            tween.Step(deltaTimeS); // Play
            tween.Step(deltaTimeS); // EndPlay

            float remainingDeltaTimeS = tween.Step(deltaTimeS);
            TweenState state = tween.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onEndIteration.Received(1).Invoke();
        }

        [TestCase(-1, TweenState.PrepareRepetition, false)]
        [TestCase(2, TweenState.PrepareRepetition, false)]
        [TestCase(1, TweenState.PrepareRepetition, false)]
        [TestCase(0, TweenState.Complete, true)]
        public void Step_EndIteration_ExpectedBothStateAndCallback(int repetitions, TweenState expectedState, bool onCompleteCalled)
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float delayAfterS = 0.0f;
            const float durationS = 0.75f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            ITween tween = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, repetitions: repetitions, durationS: durationS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration
            tween.Step(deltaTimeS); // WaitBefore
            tween.Step(deltaTimeS); // StartPlay
            tween.Step(deltaTimeS); // Play
            tween.Step(deltaTimeS); // EndPlay
            tween.Step(deltaTimeS); // WaitAfter

            float remainingDeltaTimeS = tween.Step(deltaTimeS);
            TweenState state = tween.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            if (onCompleteCalled)
            {
                _onComplete.Received(1).Invoke();
            }
            else
            {
                _onComplete.DidNotReceive().Invoke();
            }
        }

        [Test]
        public void Step_PrepareRepetition_StartIterationAndCallback()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float delayAfterS = 0.0f;
            const int repetitions = -1;
            const float durationS = 0.75f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            const TweenState expectedState = TweenState.StartIteration;
            ITween tween = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, repetitions: repetitions, durationS: durationS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration

            _onStartIteration.Received(1).Invoke();

            tween.Step(deltaTimeS); // WaitBefore
            tween.Step(deltaTimeS); // StartPlay
            tween.Step(deltaTimeS); // Play
            tween.Step(deltaTimeS); // EndPlay
            tween.Step(deltaTimeS); // WaitAfter
            tween.Step(deltaTimeS); // EndIteration

            float remainingDeltaTimeS = tween.Step(deltaTimeS);
            TweenState state = tween.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onStartIteration.Received(2).Invoke();
        }

        [Test]
        public void Step_Complete_ReturnsSameAndComplete()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float delayAfterS = 0.0f;
            const int repetitions = 0;
            const float durationS = 0.75f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            const TweenState expectedState = TweenState.Complete;
            ITween tween = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, repetitions: repetitions, durationS: durationS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration
            tween.Step(deltaTimeS); // WaitBefore
            tween.Step(deltaTimeS); // StartPlay
            tween.Step(deltaTimeS); // Play
            tween.Step(deltaTimeS); // EndPlay
            tween.Step(deltaTimeS); // WaitAfter
            tween.Step(deltaTimeS); // EndIteration

            float remainingDeltaTimeS = tween.Step(deltaTimeS);
            TweenState state = tween.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
        }

        [Test]
        public void Pause_NotPaused_PausedAndCallback()
        {
            ITween tween = Build();

            tween.Pause();

            Assert.IsTrue(tween.Paused);
            _onPause.Received(1).Invoke();
        }

        [Test]
        public void Pause_Paused_NoCallback()
        {
            ITween tween = Build();
            tween.Pause();

            _onPause.Received(1).Invoke();

            tween.Pause();

            _onPause.Received(1).Invoke();
        }

        [Test]
        public void Resume_Paused_NotPausedAndCallback()
        {
            ITween tween = Build();
            tween.Pause();

            tween.Resume();

            Assert.IsFalse(tween.Paused);
            _onResume.Received(1).Invoke();
        }

        [Test]
        public void Resume_NotPaused_NoCallback()
        {
            ITween tween = Build();

            tween.Resume();

            _onResume.DidNotReceive().Invoke();
        }

        [Test]
        public void Restart_StartIterationAndCallback()
        {
            const TweenState expectedState = TweenState.StartIteration;
            ITween tween = Build();

            tween.Restart();
            TweenState state = tween.State;

            Assert.AreEqual(expectedState, state);
            _onRestart.Received(1).Invoke();
        }

        [Test]
        public void Restart_NotPaused_NotPaused()
        {
            ITween tween = Build();

            tween.Restart();

            Assert.IsFalse(tween.Paused);
        }

        [Test]
        public void Restart_Paused_Paused()
        {
            ITween tween = Build();
            tween.Pause();

            tween.Restart();

            Assert.IsTrue(tween.Paused);
        }

        #endregion

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