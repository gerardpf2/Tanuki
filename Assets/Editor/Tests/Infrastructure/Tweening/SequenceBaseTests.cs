using System;
using System.Collections.Generic;
using Infrastructure.Tweening;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class SequenceBaseTests
    {
        private ICollection<ITween> _tweens;

        [SetUp]
        public void SetUp()
        {
            _tweens = new List<ITween>
            {
                Substitute.For<ITween>(),
                Substitute.For<ITween>(),
                Substitute.For<ITween>()
            };
        }

        [Test]
        public void Step_PrepareRepetition_TweensRestartCalled()
        {
            const int repetitions = -1;
            const float playRemainingDeltaTimeS = 0.1f;
            const float deltaTimeS = 1.0f;
            SequenceBase sequenceBase = Build(repetitions: repetitions, playRemainingDeltaTimeS: playRemainingDeltaTimeS);
            sequenceBase.Step(deltaTimeS); // SetUp
            sequenceBase.Step(deltaTimeS); // StartIteration
            sequenceBase.Step(deltaTimeS); // WaitBefore
            sequenceBase.Step(deltaTimeS); // StartPlay
            sequenceBase.Step(deltaTimeS); // Play
            sequenceBase.Step(deltaTimeS); // EndPlay
            sequenceBase.Step(deltaTimeS); // WaitAfter
            sequenceBase.Step(deltaTimeS); // EndIteration

            sequenceBase.Step(deltaTimeS);

            foreach (ITween tween in _tweens)
            {
                tween.Received(1).Restart();
            }
        }

        [Test]
        public void Restart_TweensRestartCalled()
        {
            SequenceBase sequenceBase = Build();

            sequenceBase.Restart();

            foreach (ITween tween in _tweens)
            {
                tween.Received(1).Restart();
            }
        }

        // Equals Null / ReferenceEquals already tested

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            SequenceBase sequenceBase = Build();
            NotASequenceBase other = new();

            Assert.AreNotEqual(sequenceBase, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            SequenceBase sequenceBase = Build();
            SequenceBase other = Build();

            Assert.AreEqual(sequenceBase, other);
        }

        [Test]
        public void Equals_OtherDifferentParams_ReturnsFalse()
        {
            IEnumerable<ITween> otherTweens = new List<ITween>();
            SequenceBase sequenceBase = Build();
            SequenceBase other = Build(tweens: otherTweens);

            Assert.AreNotEqual(sequenceBase, other);
        }

        [Test]
        public void GetHashCode_OtherSameParams_SameReturnedValue()
        {
            SequenceBase sequenceBase = Build();
            SequenceBase other = Build();

            Assert.AreEqual(sequenceBase.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams_DifferentReturnedValue()
        {
            IEnumerable<ITween> otherTweens = new List<ITween>();
            SequenceBase sequenceBase = Build();
            SequenceBase other = Build(tweens: otherTweens);

            Assert.AreNotEqual(sequenceBase.GetHashCode(), other.GetHashCode());
        }

        private SequenceBase Build(
            IEnumerable<ITween> tweens = null,
            int repetitions = 0,
            float playRemainingDeltaTimeS = 0.0f)
        {
            Func<float> play = Substitute.For<Func<float>>();
            play.Invoke().Returns(playRemainingDeltaTimeS);

            return
                new SequenceBaseTesting(
                    true,
                    0.0f,
                    0.0f,
                    repetitions,
                    RepetitionType.Restart,
                    DelayManagement.BeforeAndAfter,
                    DelayManagement.BeforeAndAfter,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    tweens ?? _tweens,
                    play
                );
        }

        #region SequenceBaseTesting

        // Since SequenceBase is abstract, SequenceBaseTesting (inherits from SequenceBase and has no logic) needs to be used

        private sealed class SequenceBaseTesting : SequenceBase
        {
            private readonly Func<float> _play;

            public SequenceBaseTesting(
                bool autoPlay,
                float delayBeforeS,
                float delayAfterS,
                int repetitions,
                RepetitionType repetitionType,
                DelayManagement delayManagementRepetition,
                DelayManagement delayManagementRestart,
                Action onStartIteration,
                Action onStartPlay,
                Action onEndPlay,
                Action onEndIteration,
                Action onPause,
                Action onResume,
                Action onRestart,
                Action onComplete,
                IEnumerable<ITween> tweens,
                Func<float> play) : base(autoPlay, delayBeforeS, delayAfterS, repetitions, repetitionType, delayManagementRepetition, delayManagementRestart, onStartIteration, onStartPlay, onEndPlay, onEndIteration, onPause, onResume, onRestart, onComplete, tweens)
            {
                _play = play;
            }

            protected override float Play(float deltaTimeS, bool backwards, IReadOnlyList<ITween> tweens)
            {
                return _play();
            }
        }

        private sealed class NotASequenceBase : TweenBase
        {
            public NotASequenceBase() : base(true, 0.0f, 0.0f, 0, RepetitionType.Restart, DelayManagement.BeforeAndAfter, DelayManagement.BeforeAndAfter, null, null, null, null, null, null, null, null) { }

            protected override float Play(float deltaTimeS, bool backwards)
            {
                return 0.0f;
            }
        }

        #endregion
    }
}