using System.Collections.Generic;
using Infrastructure.Tweening;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class SequenceAsyncTests
    {
        private IReadOnlyList<ITweenBase> _tweens;

        [SetUp]
        public void SetUp()
        {
            _tweens = new List<ITweenBase>
            {
                Substitute.For<ITweenBase>(),
                Substitute.For<ITweenBase>(),
                Substitute.For<ITweenBase>()
            };
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Step_Play_ReturnsMinRemainingDeltaTimeAndNoUnexpectedCalls(bool backwards)
        {
            const float deltaTimeS = 1.0f;
            const float firstRemainingDeltaTimeS = 0.75f;
            const float middleRemainingDeltaTimeS = 0.5f;
            const float lastRemainingDeltaTimeS = 0.25f;
            float expectedRemainingDeltaTimeS = Mathf.Min(firstRemainingDeltaTimeS, middleRemainingDeltaTimeS, lastRemainingDeltaTimeS);
            _tweens[0].Update(deltaTimeS, backwards).Returns(firstRemainingDeltaTimeS);
            _tweens[1].Update(deltaTimeS, backwards).Returns(middleRemainingDeltaTimeS);
            _tweens[2].Update(deltaTimeS, backwards).Returns(lastRemainingDeltaTimeS);
            SequenceAsync sequenceAsync = Build();
            sequenceAsync.Step(deltaTimeS); // SetUp
            sequenceAsync.Step(deltaTimeS); // StartIteration
            sequenceAsync.Step(deltaTimeS); // WaitBefore
            sequenceAsync.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = sequenceAsync.Step(deltaTimeS, backwards);

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            _tweens[0].DidNotReceive().Update(Arg.Is<float>(x => !Mathf.Approximately(x, deltaTimeS)), Arg.Any<bool>());
            _tweens[1].DidNotReceive().Update(Arg.Is<float>(x => !Mathf.Approximately(x, deltaTimeS)), Arg.Any<bool>());
            _tweens[2].DidNotReceive().Update(Arg.Is<float>(x => !Mathf.Approximately(x, deltaTimeS)), Arg.Any<bool>());
        }

        // Equals Null / ReferenceEquals already tested

        [Test]
        public void Equals_OtherWrongTypeAndSameParams_ReturnsFalse()
        {
            SequenceAsync sequenceAsync = Build();
            Sequence other = new(true, 0.0f, 0.0f, 0, RepetitionType.Restart, DelayManagement.BeforeAndAfter, DelayManagement.BeforeAndAfter, null, null, null, null, null, null, null, null, null, null, _tweens);

            Assert.AreNotEqual(sequenceAsync, other);
        }

        [Test]
        public void GetHashCode_OtherWrongTypeAndSameParams_DifferentReturnedValue()
        {
            SequenceAsync sequenceAsync = Build();
            Sequence other = new(true, 0.0f, 0.0f, 0, RepetitionType.Restart, DelayManagement.BeforeAndAfter, DelayManagement.BeforeAndAfter, null, null, null, null, null, null, null, null, null, null, _tweens);

            Assert.AreNotEqual(sequenceAsync.GetHashCode(), other.GetHashCode());
        }

        private SequenceAsync Build()
        {
            return
                new SequenceAsync(
                    true,
                    0.0f,
                    0.0f,
                    0,
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
                    null,
                    null,
                    _tweens
                );
        }
    }
}