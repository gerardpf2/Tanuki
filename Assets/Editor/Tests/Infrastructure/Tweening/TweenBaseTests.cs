using System;
using Infrastructure.Tweening;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class TweenBaseTests
    {
        private Action _onStartIteration;
        private Action _onStartPlay;
        private Action _onPlay;
        private Action _onEndPlay;
        private Action _onEndIteration;
        private Action _onComplete;
        private Action _onPause;
        private Action _onResume;
        private Action _onRestart;

        [SetUp]
        public void SetUp()
        {
            _onStartIteration = Substitute.For<Action>();
            _onStartPlay = Substitute.For<Action>();
            _onPlay = Substitute.For<Action>();
            _onEndPlay = Substitute.For<Action>();
            _onEndIteration = Substitute.For<Action>();
            _onComplete = Substitute.For<Action>();
            _onPause = Substitute.For<Action>();
            _onResume = Substitute.For<Action>();
            _onRestart = Substitute.For<Action>();
        }

        [Test]
        public void Step_Paused_ReturnsZero()
        {
            const bool autoPlay = false;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = 0.0f;
            TweenBase tweenBase = Build(autoPlay: autoPlay);
            tweenBase.Step(deltaTimeS); // SetUp

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
        }

        [Test]
        public void Step_SetUpAndAutoPlayTrue_StartIterationAndCallbackAndNotPaused()
        {
            const bool autoPlay = true;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            const TweenState expectedState = TweenState.StartIteration;
            TweenBase tweenBase = Build(autoPlay: autoPlay);

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onStartIteration.Received(1).Invoke();
            Assert.IsFalse(tweenBase.Paused);
        }

        [Test]
        public void Step_SetUpAndAutoPlayFalse_StartIterationAndCallbackAndPaused()
        {
            const bool autoPlay = false;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            const TweenState expectedState = TweenState.StartIteration;
            TweenBase tweenBase = Build(autoPlay: autoPlay);

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onStartIteration.Received(1).Invoke();
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
            TweenBase tweenBase = Build(autoPlay: autoPlay, delayManagementRestart: delayManagement);
            tweenBase.Restart(); // Apply delayManagement and move to StartIteration

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

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
            TweenBase tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

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
            TweenBase tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

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
            TweenBase tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS);
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
            TweenBase tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration
            tweenBase.Step(deltaTimeS); // WaitBefore
            tweenBase.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onPlay.Received(1).Invoke();
            _onEndPlay.DidNotReceive().Invoke();
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
            TweenBase tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration
            tweenBase.Step(deltaTimeS); // WaitBefore
            tweenBase.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = tweenBase.Step(deltaTimeS);
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            Assert.AreEqual(expectedState, state);
            _onPlay.Received(1).Invoke();
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
            const float playRemainingDeltaTimeS = 0.1f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            TweenBase tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayManagementRestart: delayManagement, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
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
            const float playRemainingDeltaTimeS = 0.1f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = 0.0f;
            const TweenState expectedState = TweenState.WaitAfter;
            TweenBase tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
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
            _onEndIteration.DidNotReceive().Invoke();
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
            TweenBase tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
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
            const float playRemainingDeltaTimeS = 0.1f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            TweenBase tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, repetitions: repetitions, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
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
            const float playRemainingDeltaTimeS = 0.1f;
            const float deltaTimeS = 1.0f;
            const float expectedRemainingDeltaTimeS = deltaTimeS;
            const TweenState expectedState = TweenState.StartIteration;
            TweenBase tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, repetitions: repetitions, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
            tweenBase.Step(deltaTimeS); // SetUp
            tweenBase.Step(deltaTimeS); // StartIteration

            _onStartIteration.Received(1).Invoke();

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
            _onStartIteration.Received(2).Invoke();
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
            TweenBase tweenBase = Build(autoPlay: autoPlay, delayBeforeS: delayBeforeS, delayAfterS: delayAfterS, repetitions: repetitions, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
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
            TweenBase tweenBase = Build();

            tweenBase.Pause();

            Assert.IsTrue(tweenBase.Paused);
            _onPause.Received(1).Invoke();
        }

        [Test]
        public void Pause_Paused_NoCallback()
        {
            TweenBase tweenBase = Build();
            tweenBase.Pause();

            _onPause.Received(1).Invoke();

            tweenBase.Pause();

            _onPause.Received(1).Invoke();
        }

        [Test]
        public void Resume_Paused_NotPausedAndCallback()
        {
            TweenBase tweenBase = Build();
            tweenBase.Pause();

            tweenBase.Resume();

            Assert.IsFalse(tweenBase.Paused);
            _onResume.Received(1).Invoke();
        }

        [Test]
        public void Resume_NotPaused_NoCallback()
        {
            TweenBase tweenBase = Build();

            tweenBase.Resume();

            _onResume.DidNotReceive().Invoke();
        }

        [Test]
        public void Restart_StartIterationAndCallback()
        {
            const TweenState expectedState = TweenState.StartIteration;
            TweenBase tweenBase = Build();

            tweenBase.Restart();
            TweenState state = tweenBase.State;

            Assert.AreEqual(expectedState, state);
            _onRestart.Received(1).Invoke();
        }

        [Test]
        public void Restart_NotPaused_NotPaused()
        {
            TweenBase tweenBase = Build();

            tweenBase.Restart();

            Assert.IsFalse(tweenBase.Paused);
        }

        [Test]
        public void Restart_Paused_Paused()
        {
            TweenBase tweenBase = Build();
            tweenBase.Pause();

            tweenBase.Restart();

            Assert.IsTrue(tweenBase.Paused);
        }

        [Test]
        public void Equals_OtherNull_ReturnsFalse()
        {
            TweenBase tweenBase = Build();
            const TweenBase other = null;

            Assert.IsFalse(tweenBase.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_SameRef_ReturnsTrue()
        {
            TweenBase tweenBase = Build();
            TweenBase other = tweenBase;

            Assert.IsTrue(tweenBase.Equals(other)); // Assert.AreEqual cannot be used in here
        }

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            TweenBase tweenBase = Build();
            NotATweenBase other = new();

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            TweenBase tweenBase = Build();
            TweenBase other = Build();

            Assert.AreEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams1_ReturnsFalse()
        {
            const bool autoPlay = true;
            const bool otherAutoPlay = false;
            TweenBase tweenBase = Build(autoPlay: autoPlay);
            TweenBase other = Build(autoPlay: otherAutoPlay);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams2_ReturnsFalse()
        {
            const float delayBeforeS = 1.0f;
            const float otherDelayBeforeS = 2.0f;
            TweenBase tweenBase = Build(delayBeforeS: delayBeforeS);
            TweenBase other = Build(delayBeforeS: otherDelayBeforeS);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams3_ReturnsFalse()
        {
            const float delayAfterS = 1.0f;
            const float otherDelayAfterS = 2.0f;
            TweenBase tweenBase = Build(delayAfterS: delayAfterS);
            TweenBase other = Build(delayAfterS: otherDelayAfterS);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams4_ReturnsFalse()
        {
            const int repetitions = 1;
            const int otherRepetitions = 2;
            TweenBase tweenBase = Build(repetitions: repetitions);
            TweenBase other = Build(repetitions: otherRepetitions);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams5_ReturnsFalse()
        {
            const RepetitionType repetitionType = RepetitionType.Restart;
            const RepetitionType otherRepetitionType = RepetitionType.Yoyo;
            TweenBase tweenBase = Build(repetitionType: repetitionType);
            TweenBase other = Build(repetitionType: otherRepetitionType);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams6_ReturnsFalse()
        {
            const DelayManagement delayManagementRepetition = DelayManagement.Before;
            const DelayManagement otherDelayManagementRepetition = DelayManagement.After;
            TweenBase tweenBase = Build(delayManagementRepetition: delayManagementRepetition);
            TweenBase other = Build(delayManagementRepetition: otherDelayManagementRepetition);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams7_ReturnsFalse()
        {
            const DelayManagement delayManagementRestart = DelayManagement.Before;
            const DelayManagement otherDelayManagementRestart = DelayManagement.After;
            TweenBase tweenBase = Build(delayManagementRestart: delayManagementRestart);
            TweenBase other = Build(delayManagementRestart: otherDelayManagementRestart);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams8_ReturnsFalse()
        {
            Action otherOnStartIteration = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onStartIteration: otherOnStartIteration);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams9_ReturnsFalse()
        {
            Action otherOnStartPlay = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onStartPlay: otherOnStartPlay);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams10_ReturnsFalse()
        {
            Action otherOnPlay = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onPlay: otherOnPlay);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams11_ReturnsFalse()
        {
            Action otherOnEndPlay = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onEndPlay: otherOnEndPlay);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams12_ReturnsFalse()
        {
            Action otherOnEndIteration = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onEndIteration: otherOnEndIteration);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams13_ReturnsFalse()
        {
            Action otherOnComplete = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onComplete: otherOnComplete);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams14_ReturnsFalse()
        {
            Action otherOnPause = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onPause: otherOnPause);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams15_ReturnsFalse()
        {
            Action otherOnResume = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onResume: otherOnResume);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams16_ReturnsFalse()
        {
            Action otherOnRestart = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onRestart: otherOnRestart);

            Assert.AreNotEqual(tweenBase, other);
        }

        [Test]
        public void GetHashCode_OtherSameParams_SameReturnedValue()
        {
            TweenBase tweenBase = Build();
            TweenBase other = Build();

            Assert.AreEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams1_DifferentReturnedValue()
        {
            const bool autoPlay = true;
            const bool otherAutoPlay = false;
            TweenBase tweenBase = Build(autoPlay: autoPlay);
            TweenBase other = Build(autoPlay: otherAutoPlay);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams2_DifferentReturnedValue()
        {
            const float delayBeforeS = 1.0f;
            const float otherDelayBeforeS = 2.0f;
            TweenBase tweenBase = Build(delayBeforeS: delayBeforeS);
            TweenBase other = Build(delayBeforeS: otherDelayBeforeS);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams3_DifferentReturnedValue()
        {
            const float delayAfterS = 1.0f;
            const float otherDelayAfterS = 2.0f;
            TweenBase tweenBase = Build(delayAfterS: delayAfterS);
            TweenBase other = Build(delayAfterS: otherDelayAfterS);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams4_DifferentReturnedValue()
        {
            const int repetitions = 1;
            const int otherRepetitions = 2;
            TweenBase tweenBase = Build(repetitions: repetitions);
            TweenBase other = Build(repetitions: otherRepetitions);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams5_DifferentReturnedValue()
        {
            const RepetitionType repetitionType = RepetitionType.Restart;
            const RepetitionType otherRepetitionType = RepetitionType.Yoyo;
            TweenBase tweenBase = Build(repetitionType: repetitionType);
            TweenBase other = Build(repetitionType: otherRepetitionType);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams6_DifferentReturnedValue()
        {
            const DelayManagement delayManagementRepetition = DelayManagement.Before;
            const DelayManagement otherDelayManagementRepetition = DelayManagement.After;
            TweenBase tweenBase = Build(delayManagementRepetition: delayManagementRepetition);
            TweenBase other = Build(delayManagementRepetition: otherDelayManagementRepetition);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams7_DifferentReturnedValue()
        {
            const DelayManagement delayManagementRestart = DelayManagement.Before;
            const DelayManagement otherDelayManagementRestart = DelayManagement.After;
            TweenBase tweenBase = Build(delayManagementRestart: delayManagementRestart);
            TweenBase other = Build(delayManagementRestart: otherDelayManagementRestart);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams8_DifferentReturnedValue()
        {
            Action otherOnStartIteration = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onStartIteration: otherOnStartIteration);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams9_DifferentReturnedValue()
        {
            Action otherOnStartPlay = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onStartPlay: otherOnStartPlay);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams10_DifferentReturnedValue()
        {
            Action otherOnPlay = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onPlay: otherOnPlay);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams11_DifferentReturnedValue()
        {
            Action otherOnEndPlay = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onEndPlay: otherOnEndPlay);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams12_DifferentReturnedValue()
        {
            Action otherOnEndIteration = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onEndIteration: otherOnEndIteration);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams13_DifferentReturnedValue()
        {
            Action otherOnComplete = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onComplete: otherOnComplete);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams14_DifferentReturnedValue()
        {
            Action otherOnPause = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onPause: otherOnPause);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams15_DifferentReturnedValue()
        {
            Action otherOnResume = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onResume: otherOnResume);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams16_DifferentReturnedValue()
        {
            Action otherOnRestart = Substitute.For<Action>();
            TweenBase tweenBase = Build();
            TweenBase other = Build(onRestart: otherOnRestart);

            Assert.AreNotEqual(tweenBase.GetHashCode(), other.GetHashCode());
        }

        private TweenBase Build(
            bool autoPlay = true,
            float delayBeforeS = 0.0f,
            float delayAfterS = 0.0f,
            int repetitions = 0,
            RepetitionType repetitionType = RepetitionType.Restart,
            DelayManagement delayManagementRepetition = DelayManagement.BeforeAndAfter,
            DelayManagement delayManagementRestart = DelayManagement.BeforeAndAfter,
            Action onStartIteration = null,
            Action onStartPlay = null,
            Action onPlay = null,
            Action onEndPlay = null,
            Action onEndIteration = null,
            Action onComplete = null,
            Action onPause = null,
            Action onResume = null,
            Action onRestart = null,
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

        private sealed class TweenBaseTesting : TweenBase
        {
            private readonly Func<float> _play;

            public TweenBaseTesting(
                bool autoPlay,
                float delayBeforeS,
                float delayAfterS,
                int repetitions,
                RepetitionType repetitionType,
                DelayManagement delayManagementRepetition,
                DelayManagement delayManagementRestart,
                Action onStartIteration,
                Action onStartPlay,
                Action onPlay,
                Action onEndPlay,
                Action onEndIteration,
                Action onComplete,
                Action onPause,
                Action onResume,
                Action onRestart,
                Func<float> play) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onStartIteration, onStartPlay, onPlay, onEndPlay, onEndIteration, onComplete, onPause, onResume, onRestart)
            {
                _play = play;
            }

            protected override float Play(float deltaTimeS, bool backwards)
            {
                return _play();
            }
        }

        private sealed class NotATweenBase : ITween
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