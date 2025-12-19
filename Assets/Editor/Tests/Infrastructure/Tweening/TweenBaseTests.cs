using System;
using Infrastructure.Tweening;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class TweenBaseTests
    {
        private Action<ITweenBase> _onStep;
        private Action<ITweenBase> _onStartIteration;
        private Action<ITweenBase> _onStartPlay;
        private Action<ITweenBase> _onPlay;
        private Action<ITweenBase> _onEndPlay;
        private Action<ITweenBase> _onEndIteration;
        private Action<ITweenBase> _onComplete;
        private Action<ITweenBase> _onPause;
        private Action<ITweenBase> _onResume;
        private Action<ITweenBase> _onRestart;

        [SetUp]
        public void SetUp()
        {
            _onStep = Substitute.For<Action<ITweenBase>>();
            _onStartIteration = Substitute.For<Action<ITweenBase>>();
            _onStartPlay = Substitute.For<Action<ITweenBase>>();
            _onPlay = Substitute.For<Action<ITweenBase>>();
            _onEndPlay = Substitute.For<Action<ITweenBase>>();
            _onEndIteration = Substitute.For<Action<ITweenBase>>();
            _onComplete = Substitute.For<Action<ITweenBase>>();
            _onPause = Substitute.For<Action<ITweenBase>>();
            _onResume = Substitute.For<Action<ITweenBase>>();
            _onRestart = Substitute.For<Action<ITweenBase>>();
        }

        [Test]
        public void Step_Paused_ReturnsZero()
        {
            const bool autoPlay = false;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = 0.0f;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay);
            tweenBase.Step(deltaTimeS); // SetUp

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
        }

        [Test]
        public void Step_Paused_NoOnStepCallback()
        {
            const bool autoPlay = false;
            const float deltaTimeS = 1.0f;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay);
            tweenBase.Step(deltaTimeS); // SetUp, tween gets paused and onStep gets called

            _onStep.Received(1).Invoke(tweenBase);

            tweenBase.Step(deltaTimeS); // Paused

            _onStep.Received(1).Invoke(tweenBase);
        }

        [Test]
        public void Step_NotPaused_OnStepCallback()
        {
            const bool autoPlay = true;
            const float deltaTimeS = 1.0f;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay);
            tweenBase.Step(deltaTimeS); // SetUp, tween gets paused and onStep gets called

            _onStep.Received(1).Invoke(tweenBase);

            tweenBase.Step(deltaTimeS); // Not paused

            _onStep.Received(2).Invoke(tweenBase);
        }

        [Test]
        public void Step_SetUpAndAutoPlayTrue_StartIterationAndCallbackAndNotPaused()
        {
            const bool autoPlay = true;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            const TweenState expectedState = TweenState.StartIteration;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay);

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onStartIteration.Received(1).Invoke(tweenBase);
            Assert.IsFalse(tweenBase.Paused);
        }

        [Test]
        public void Step_SetUpAndAutoPlayFalse_StartIterationAndCallbackAndPaused()
        {
            const bool autoPlay = false;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            const TweenState expectedState = TweenState.StartIteration;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay);

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onStartIteration.Received(1).Invoke(tweenBase);
            Assert.IsTrue(tweenBase.Paused);
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
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay, delayManagementRestart: delayManagement);
            tweenBase.Restart(); // Apply delayManagement and move to StartIteration

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            if (onStartPlayCalled)
            {
                _onStartPlay.Received(1).Invoke(tweenBase);
            }
            else
            {
                _onStartPlay.DidNotReceive().Invoke(Arg.Any<ITweenBase>());
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
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onStartPlay.DidNotReceive().Invoke(Arg.Any<ITweenBase>());
        }

        [Test]
        public void Step_WaitBeforeAndTimeReached_StartPlayAndCallback()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.75f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = 0.25f;
            const TweenState expectedState = TweenState.StartPlay;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onStartPlay.Received(1).Invoke(tweenBase);
        }

        [Test]
        public void Step_StartPlay_Play()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            const TweenState expectedState = TweenState.Play;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration
            tweenBase.Step(deltaTimeS); // WaitBefore

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
        }

        [Test]
        public void Step_PlayAndPlayRemainingDeltaTimeZero_PlayAndNoEndPlayCallback()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float playRemainingDeltaTimeS = 0.0f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = playRemainingDeltaTimeS;
            const TweenState expectedState = TweenState.Play;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration
            tweenBase.Step(deltaTimeS); // WaitBefore
            tweenBase.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onPlay.Received(1).Invoke(tweenBase);
            _onEndPlay.DidNotReceive().Invoke(Arg.Any<ITweenBase>());
        }

        [Test]
        public void Step_PlayAndPlayRemainingDeltaTimeBiggerThanZero_EndPlayAndEndPlayCallback()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float playRemainingDeltaTimeS = 0.1f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = playRemainingDeltaTimeS;
            const TweenState expectedState = TweenState.EndPlay;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration
            tweenBase.Step(deltaTimeS); // WaitBefore
            tweenBase.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onPlay.Received(1).Invoke(tweenBase);
            _onEndPlay.Received(1).Invoke(tweenBase);
        }

        [TestCase(DelayManagement.BeforeAndAfter, TweenState.WaitAfter, false)]
        [TestCase(DelayManagement.Before, TweenState.EndIteration, true)]
        [TestCase(DelayManagement.After, TweenState.WaitAfter, false)]
        [TestCase(DelayManagement.None, TweenState.EndIteration, true)]
        public void Step_EndPlay_ExpectedBothStateAndCallback(DelayManagement delayManagement, TweenState expectedState, bool onEndIterationCalled)
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float playRemainingDeltaTimeS = 0.1f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayManagementRestart: delayManagement, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
            tweenBase.Restart(); // Apply delayManagement and move to StartIteration
            tweenBase.Step(deltaTimeS); // StartIteration
            if (delayManagement is DelayManagement.BeforeAndAfter or DelayManagement.Before)
            {
                tweenBase.Step(deltaTimeS); // WaitBefore
            }
            tweenBase.Step(deltaTimeS); // StartPlay
            tweenBase.Step(deltaTimeS); // Play

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            if (onEndIterationCalled)
            {
                _onEndIteration.Received(1).Invoke(tweenBase);
            }
            else
            {
                _onEndIteration.DidNotReceive().Invoke(Arg.Any<ITweenBase>());
            }
        }

        [Test]
        public void Step_WaitAfterAndTimeNotReached_WaitAfterAndNoCallback()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float delayAfterS = 1.5f;
            const float playRemainingDeltaTimeS = 0.1f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = 0.0f;
            const TweenState expectedState = TweenState.WaitAfter;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration
            tweenBase.Step(deltaTimeS); // WaitBefore
            tweenBase.Step(deltaTimeS); // StartPlay
            tweenBase.Step(deltaTimeS); // Play
            tweenBase.Step(deltaTimeS); // EndPlay

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onEndIteration.DidNotReceive().Invoke(Arg.Any<ITweenBase>());
        }

        [Test]
        public void Step_WaitAfterAndTimeReached_EndIterationAndCallback()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float delayAfterS = 0.75f;
            const float playRemainingDeltaTimeS = 0.1f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = 0.25f;
            const TweenState expectedState = TweenState.EndIteration;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration
            tweenBase.Step(deltaTimeS); // WaitBefore
            tweenBase.Step(deltaTimeS); // StartPlay
            tweenBase.Step(deltaTimeS); // Play
            tweenBase.Step(deltaTimeS); // EndPlay

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onEndIteration.Received(1).Invoke(tweenBase);
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
            const float playRemainingDeltaTimeS = 0.1f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, repetitions: repetitions, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration
            tweenBase.Step(deltaTimeS); // WaitBefore
            tweenBase.Step(deltaTimeS); // StartPlay
            tweenBase.Step(deltaTimeS); // Play
            tweenBase.Step(deltaTimeS); // EndPlay
            tweenBase.Step(deltaTimeS); // WaitAfter

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            if (onCompleteCalled)
            {
                _onComplete.Received(1).Invoke(tweenBase);
            }
            else
            {
                _onComplete.DidNotReceive().Invoke(Arg.Any<ITweenBase>());
            }
        }

        [Test]
        public void Step_PrepareRepetition_StartIterationAndCallback()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float delayAfterS = 0.0f;
            const int repetitions = -1;
            const float playRemainingDeltaTimeS = 0.1f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            const TweenState expectedState = TweenState.StartIteration;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, repetitions: repetitions, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration

            _onStartIteration.Received(1).Invoke(tweenBase);

            tweenBase.Step(deltaTimeS); // WaitBefore
            tweenBase.Step(deltaTimeS); // StartPlay
            tweenBase.Step(deltaTimeS); // Play
            tweenBase.Step(deltaTimeS); // EndPlay
            tweenBase.Step(deltaTimeS); // WaitAfter
            tweenBase.Step(deltaTimeS); // EndIteration

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onStartIteration.Received(2).Invoke(tweenBase);
        }

        [Test]
        public void Step_Complete_ReturnsSameAndComplete()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 0.0f;
            const float delayAfterS = 0.0f;
            const int repetitions = 0;
            const float playRemainingDeltaTimeS = 0.1f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            const TweenState expectedState = TweenState.Complete;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, repetitions: repetitions, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration
            tweenBase.Step(deltaTimeS); // WaitBefore
            tweenBase.Step(deltaTimeS); // StartPlay
            tweenBase.Step(deltaTimeS); // Play
            tweenBase.Step(deltaTimeS); // EndPlay
            tweenBase.Step(deltaTimeS); // WaitAfter
            tweenBase.Step(deltaTimeS); // EndIteration

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
        }

        [Test]
        public void Pause_NotPaused_PausedAndCallback()
        {
            TweenBaseTesting tweenBase = Build();

            tweenBase.Pause();

            Assert.IsTrue(tweenBase.Paused);
            _onPause.Received(1).Invoke(tweenBase);
        }

        [Test]
        public void Pause_Paused_NoCallback()
        {
            TweenBaseTesting tweenBase = Build();
            tweenBase.Pause();

            _onPause.Received(1).Invoke(tweenBase);

            tweenBase.Pause();

            _onPause.Received(1).Invoke(tweenBase);
        }

        [Test]
        public void Resume_Paused_NotPausedAndCallback()
        {
            TweenBaseTesting tweenBase = Build();
            tweenBase.Pause();

            tweenBase.Resume();

            Assert.IsFalse(tweenBase.Paused);
            _onResume.Received(1).Invoke(tweenBase);
        }

        [Test]
        public void Resume_NotPaused_NoCallback()
        {
            TweenBaseTesting tweenBase = Build();

            tweenBase.Resume();

            _onResume.DidNotReceive().Invoke(Arg.Any<ITweenBase>());
        }

        [Test]
        public void Restart_StartIterationAndCallback()
        {
            const TweenState expectedState = TweenState.StartIteration;
            TweenBaseTesting tweenBase = Build();

            tweenBase.Restart();
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedState, state);
            _onRestart.Received(1).Invoke(tweenBase);
        }

        [Test]
        public void Restart_NotPaused_NotPaused()
        {
            TweenBaseTesting tweenBase = Build();

            tweenBase.Restart();

            Assert.IsFalse(tweenBase.Paused);
        }

        [Test]
        public void Restart_Paused_Paused()
        {
            TweenBaseTesting tweenBase = Build();
            tweenBase.Pause();

            tweenBase.Restart();

            Assert.IsTrue(tweenBase.Paused);
        }

        [Test]
        public void Equals_OtherNull_ReturnsFalse()
        {
            TweenBaseTesting tweenBase = Build();
            const TweenBaseTesting other = null;

            Assert.IsFalse(tweenBase.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_SameRef_ReturnsTrue()
        {
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = tweenBase;

            Assert.IsTrue(tweenBase.Equals(other)); // Assert.AreEqual cannot be used in here
        }

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            TweenBaseTesting tweenBase = Build();
            NotATweenBase other = new();

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build();

            Assert.AreEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams1_ReturnsFalse()
        {
            const bool autoPlay = true;
            const bool otherAutoPlay = false;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay);
            TweenBaseTesting other = Build(autoPlay: otherAutoPlay);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams2_ReturnsFalse()
        {
            const float delayBeforeS = 1.0f;
            const float otherDelayBeforeS = 2.0f;
            TweenBaseTesting tweenBase = Build(delayBeforeS: delayBeforeS);
            TweenBaseTesting other = Build(delayBeforeS: otherDelayBeforeS);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams3_ReturnsFalse()
        {
            const float delayAfterS = 1.0f;
            const float otherDelayAfterS = 2.0f;
            TweenBaseTesting tweenBase = Build(delayAfterS: delayAfterS);
            TweenBaseTesting other = Build(delayAfterS: otherDelayAfterS);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams4_ReturnsFalse()
        {
            const int repetitions = 1;
            const int otherRepetitions = 2;
            TweenBaseTesting tweenBase = Build(repetitions: repetitions);
            TweenBaseTesting other = Build(repetitions: otherRepetitions);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams5_ReturnsFalse()
        {
            const RepetitionType repetitionType = RepetitionType.Restart;
            const RepetitionType otherRepetitionType = RepetitionType.Yoyo;
            TweenBaseTesting tweenBase = Build(repetitionType: repetitionType);
            TweenBaseTesting other = Build(repetitionType: otherRepetitionType);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams6_ReturnsFalse()
        {
            const DelayManagement delayManagementRepetition = DelayManagement.Before;
            const DelayManagement otherDelayManagementRepetition = DelayManagement.After;
            TweenBaseTesting tweenBase = Build(delayManagementRepetition: delayManagementRepetition);
            TweenBaseTesting other = Build(delayManagementRepetition: otherDelayManagementRepetition);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams7_ReturnsFalse()
        {
            const DelayManagement delayManagementRestart = DelayManagement.Before;
            const DelayManagement otherDelayManagementRestart = DelayManagement.After;
            TweenBaseTesting tweenBase = Build(delayManagementRestart: delayManagementRestart);
            TweenBaseTesting other = Build(delayManagementRestart: otherDelayManagementRestart);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams8_ReturnsFalse()
        {
            Action<ITweenBase> otherOnStep = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onStep: otherOnStep);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams9_ReturnsFalse()
        {
            Action<ITweenBase> otherOnStartIteration = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onStartIteration: otherOnStartIteration);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams10_ReturnsFalse()
        {
            Action<ITweenBase> otherOnStartPlay = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onStartPlay: otherOnStartPlay);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams11_ReturnsFalse()
        {
            Action<ITweenBase> otherOnPlay = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onPlay: otherOnPlay);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams12_ReturnsFalse()
        {
            Action<ITweenBase> otherOnEndPlay = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onEndPlay: otherOnEndPlay);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams13_ReturnsFalse()
        {
            Action<ITweenBase> otherOnEndIteration = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onEndIteration: otherOnEndIteration);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams14_ReturnsFalse()
        {
            Action<ITweenBase> otherOnComplete = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onComplete: otherOnComplete);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams15_ReturnsFalse()
        {
            Action<ITweenBase> otherOnPause = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onPause: otherOnPause);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams16_ReturnsFalse()
        {
            Action<ITweenBase> otherOnResume = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onResume: otherOnResume);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams17_ReturnsFalse()
        {
            Action<ITweenBase> otherOnRestart = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onRestart: otherOnRestart);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void GetHashCode_OtherSameParams_SameReturnedValue()
        {
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build();

            Assert.AreEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams1_DifferentReturnedValue()
        {
            const bool autoPlay = true;
            const bool otherAutoPlay = false;
            TweenBaseTesting tweenBase = Build(autoPlay: autoPlay);
            TweenBaseTesting other = Build(autoPlay: otherAutoPlay);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams2_DifferentReturnedValue()
        {
            const float delayBeforeS = 1.0f;
            const float otherDelayBeforeS = 2.0f;
            TweenBaseTesting tweenBase = Build(delayBeforeS: delayBeforeS);
            TweenBaseTesting other = Build(delayBeforeS: otherDelayBeforeS);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams3_DifferentReturnedValue()
        {
            const float delayAfterS = 1.0f;
            const float otherDelayAfterS = 2.0f;
            TweenBaseTesting tweenBase = Build(delayAfterS: delayAfterS);
            TweenBaseTesting other = Build(delayAfterS: otherDelayAfterS);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams4_DifferentReturnedValue()
        {
            const int repetitions = 1;
            const int otherRepetitions = 2;
            TweenBaseTesting tweenBase = Build(repetitions: repetitions);
            TweenBaseTesting other = Build(repetitions: otherRepetitions);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams5_DifferentReturnedValue()
        {
            const RepetitionType repetitionType = RepetitionType.Restart;
            const RepetitionType otherRepetitionType = RepetitionType.Yoyo;
            TweenBaseTesting tweenBase = Build(repetitionType: repetitionType);
            TweenBaseTesting other = Build(repetitionType: otherRepetitionType);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams6_DifferentReturnedValue()
        {
            const DelayManagement delayManagementRepetition = DelayManagement.Before;
            const DelayManagement otherDelayManagementRepetition = DelayManagement.After;
            TweenBaseTesting tweenBase = Build(delayManagementRepetition: delayManagementRepetition);
            TweenBaseTesting other = Build(delayManagementRepetition: otherDelayManagementRepetition);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams7_DifferentReturnedValue()
        {
            const DelayManagement delayManagementRestart = DelayManagement.Before;
            const DelayManagement otherDelayManagementRestart = DelayManagement.After;
            TweenBaseTesting tweenBase = Build(delayManagementRestart: delayManagementRestart);
            TweenBaseTesting other = Build(delayManagementRestart: otherDelayManagementRestart);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams8_DifferentReturnedValue()
        {
            Action<ITweenBase> otherOnStep = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onStep: otherOnStep);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams9_DifferentReturnedValue()
        {
            Action<ITweenBase> otherOnStartIteration = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onStartIteration: otherOnStartIteration);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams10_DifferentReturnedValue()
        {
            Action<ITweenBase> otherOnStartPlay = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onStartPlay: otherOnStartPlay);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams11_DifferentReturnedValue()
        {
            Action<ITweenBase> otherOnPlay = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onPlay: otherOnPlay);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams12_DifferentReturnedValue()
        {
            Action<ITweenBase> otherOnEndPlay = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onEndPlay: otherOnEndPlay);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams13_DifferentReturnedValue()
        {
            Action<ITweenBase> otherOnEndIteration = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onEndIteration: otherOnEndIteration);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams14_DifferentReturnedValue()
        {
            Action<ITweenBase> otherOnComplete = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onComplete: otherOnComplete);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams15_DifferentReturnedValue()
        {
            Action<ITweenBase> otherOnPause = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onPause: otherOnPause);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams16_DifferentReturnedValue()
        {
            Action<ITweenBase> otherOnResume = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onResume: otherOnResume);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams17_DifferentReturnedValue()
        {
            Action<ITweenBase> otherOnRestart = Substitute.For<Action<ITweenBase>>();
            TweenBaseTesting tweenBase = Build();
            TweenBaseTesting other = Build(onRestart: otherOnRestart);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        private TweenBaseTesting Build(
            bool autoPlay = true,
            float delayBeforeS = 0.0f,
            float delayAfterS = 0.0f,
            int repetitions = 0,
            RepetitionType repetitionType = RepetitionType.Restart,
            DelayManagement delayManagementRepetition = DelayManagement.BeforeAndAfter,
            DelayManagement delayManagementRestart = DelayManagement.BeforeAndAfter,
            Action<ITweenBase> onStep = null,
            Action<ITweenBase> onStartIteration = null,
            Action<ITweenBase> onStartPlay = null,
            Action<ITweenBase> onPlay = null,
            Action<ITweenBase> onEndPlay = null,
            Action<ITweenBase> onEndIteration = null,
            Action<ITweenBase> onComplete = null,
            Action<ITweenBase> onPause = null,
            Action<ITweenBase> onResume = null,
            Action<ITweenBase> onRestart = null,
            float playRemainingDeltaTimeS = 0.0f)
        {
            Func<float> play = Substitute.For<Func<float>>();
            play.Invoke().Returns(playRemainingDeltaTimeS);

            return
                new TweenBaseTesting(
                    autoPlay,
                    delayBeforeS,
                    delayAfterS,
                    repetitions,
                    repetitionType,
                    delayManagementRepetition,
                    delayManagementRestart,
                    onStep ?? _onStep,
                    onStartIteration ?? _onStartIteration,
                    onStartPlay ?? _onStartPlay,
                    onPlay ?? _onPlay,
                    onEndPlay ?? _onEndPlay,
                    onEndIteration ?? _onEndIteration,
                    onComplete ?? _onComplete,
                    onPause ?? _onPause,
                    onResume ?? _onResume,
                    onRestart ?? _onRestart,
                    play
                );
        }

        #region TweenBaseTesting

        // Since TweenBase is abstract, TweenBaseTesting (inherits from TweenBase and has no logic) needs to be used

        private sealed class TweenBaseTesting : TweenBase<ITweenBase>
        {
            private readonly Func<float> _play;

            protected override ITweenBase This => this;

            public TweenBaseTesting(
                bool autoPlay,
                float delayBeforeS,
                float delayAfterS,
                int repetitions,
                RepetitionType repetitionType,
                DelayManagement delayManagementRepetition,
                DelayManagement delayManagementRestart,
                Action<ITweenBase> onStep,
                Action<ITweenBase> onStartIteration,
                Action<ITweenBase> onStartPlay,
                Action<ITweenBase> onPlay,
                Action<ITweenBase> onEndPlay,
                Action<ITweenBase> onEndIteration,
                Action<ITweenBase> onComplete,
                Action<ITweenBase> onPause,
                Action<ITweenBase> onResume,
                Action<ITweenBase> onRestart,
                Func<float> play) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onStep, onStartIteration, onStartPlay, onPlay, onEndPlay, onEndIteration, onComplete, onPause, onResume, onRestart)
            {
                _play = play;
            }

            protected override float Play(float deltaTimeS, bool backwards)
            {
                return _play();
            }
        }

        private sealed class NotATweenBase : ITweenBase
        {
            public TweenState State => TweenState.SetUp;

            public bool Paused => false;

            public float Step(float deltaTimeS, bool backwards = false)
            {
                return 0.0f;
            }

            public void Pause() { }

            public void Resume() { }

            public void Restart() { }
        }

        #endregion
    }
}