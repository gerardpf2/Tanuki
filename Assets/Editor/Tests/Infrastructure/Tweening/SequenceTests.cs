using System.Collections.Generic;
using Infrastructure.Tweening;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class SequenceTests
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
        public void Step_PlayAndFirstRemainingDeltaTimeZero_ReturnsZeroAndNoUnexpectedCalls(bool backwards)
        {
            const float deltaTimeS = 1.0f;
            const float firstDeltaTimeSInput = deltaTimeS;
            const float firstDeltaTimeSOutput = 0.0f;
            const float expectedRemainingDeltaTimeS = firstDeltaTimeSOutput;
            _tweens[GetIndex(0, backwards)].Update(firstDeltaTimeSInput, backwards).Returns(firstDeltaTimeSOutput);
            Sequence sequence = Build();
            sequence.Step(deltaTimeS); // SetUp
            sequence.Step(deltaTimeS); // StartIteration
            sequence.Step(deltaTimeS); // WaitBefore
            sequence.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = sequence.Step(deltaTimeS, backwards);

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            _tweens[GetIndex(0, backwards)].DidNotReceive().Update(Arg.Is<float>(x => !Mathf.Approximately(x, firstDeltaTimeSInput)), Arg.Any<bool>());
            _tweens[GetIndex(1, backwards)].DidNotReceive().Update(Arg.Any<float>(), Arg.Any<bool>());
            _tweens[GetIndex(2, backwards)].DidNotReceive().Update(Arg.Any<float>(), Arg.Any<bool>());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Step_PlayAndFirstRemainingDeltaTimeBiggerThanZeroAndMiddleRemainingDeltaTimeZero_ReturnsZeroAndNoUnexpectedCalls(bool backwards)
        {
            const float deltaTimeS = 1.0f;
            const float firstDeltaTimeSInput = deltaTimeS;
            const float firstDeltaTimeSOutput = 0.5f;
            const float middleDeltaTimeSInput = firstDeltaTimeSOutput;
            const float middleDeltaTimeSOutput = 0.0f;
            const float expectedRemainingDeltaTimeS = middleDeltaTimeSOutput;
            _tweens[GetIndex(0, backwards)].Update(firstDeltaTimeSInput, backwards).Returns(firstDeltaTimeSOutput);
            _tweens[GetIndex(1, backwards)].Update(middleDeltaTimeSInput, backwards).Returns(middleDeltaTimeSOutput);
            Sequence sequence = Build();
            sequence.Step(deltaTimeS); // SetUp
            sequence.Step(deltaTimeS); // StartIteration
            sequence.Step(deltaTimeS); // WaitBefore
            sequence.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = sequence.Step(deltaTimeS, backwards);

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            _tweens[GetIndex(0, backwards)].DidNotReceive().Update(Arg.Is<float>(x => !Mathf.Approximately(x, firstDeltaTimeSInput)), Arg.Any<bool>());
            _tweens[GetIndex(1, backwards)].DidNotReceive().Update(Arg.Is<float>(x => !Mathf.Approximately(x, middleDeltaTimeSInput)), Arg.Any<bool>());
            _tweens[GetIndex(2, backwards)].DidNotReceive().Update(Arg.Any<float>(), Arg.Any<bool>());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Step_PlayAndAllRemainingDeltaTimeBiggerThanZero_ReturnsExpectedAndNoUnexpectedCalls(bool backwards)
        {
            const float deltaTimeS = 1.0f;
            const float firstDeltaTimeSInput = deltaTimeS;
            const float firstDeltaTimeSOutput = 0.75f;
            const float middleDeltaTimeSInput = firstDeltaTimeSOutput;
            const float middleDeltaTimeSOutput = 0.5f;
            const float lastDeltaTimeSInput = middleDeltaTimeSOutput;
            const float lastDeltaTimeSOutput = 0.25f;
            const float expectedRemainingDeltaTimeS = lastDeltaTimeSOutput;
            _tweens[GetIndex(0, backwards)].Update(firstDeltaTimeSInput, backwards).Returns(firstDeltaTimeSOutput);
            _tweens[GetIndex(1, backwards)].Update(middleDeltaTimeSInput, backwards).Returns(middleDeltaTimeSOutput);
            _tweens[GetIndex(2, backwards)].Update(lastDeltaTimeSInput, backwards).Returns(lastDeltaTimeSOutput);
            Sequence sequence = Build();
            sequence.Step(deltaTimeS); // SetUp
            sequence.Step(deltaTimeS); // StartIteration
            sequence.Step(deltaTimeS); // WaitBefore
            sequence.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = sequence.Step(deltaTimeS, backwards);

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            _tweens[GetIndex(0, backwards)].DidNotReceive().Update(Arg.Is<float>(x => !Mathf.Approximately(x, firstDeltaTimeSInput)), Arg.Any<bool>());
            _tweens[GetIndex(1, backwards)].DidNotReceive().Update(Arg.Is<float>(x => !Mathf.Approximately(x, middleDeltaTimeSInput)), Arg.Any<bool>());
            _tweens[GetIndex(2, backwards)].DidNotReceive().Update(Arg.Is<float>(x => !Mathf.Approximately(x, lastDeltaTimeSInput)), Arg.Any<bool>());
        }

        // Equals Null / ReferenceEquals already tested

        [Test]
        public void Equals_OtherWrongTypeAndSameParams_ReturnsFalse()
        {
            Sequence sequence = Build();
            SequenceAsync other = new(true, 0.0f, 0.0f, 0, RepetitionType.Restart, DelayManagement.BeforeAndAfter, DelayManagement.BeforeAndAfter, null, null, null, null, null, null, null, null, null, null, _tweens);

            Assert.AreNotEqual(sequence, other);
        }

        [Test]
        public void GetHashCode_OtherWrongTypeAndSameParams_DifferentReturnedValue()
        {
            Sequence sequence = Build();
            SequenceAsync other = new(true, 0.0f, 0.0f, 0, RepetitionType.Restart, DelayManagement.BeforeAndAfter, DelayManagement.BeforeAndAfter, null, null, null, null, null, null, null, null, null, null, _tweens);

            Assert.AreNotEqual(sequence.GetHashCode(), other.GetHashCode());
        }

        private int GetIndex(int index, bool backwards)
        {
            return backwards ? _tweens.Count - 1 - index : index;
        }

        private Sequence Build()
        {
            return
                new Sequence(
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