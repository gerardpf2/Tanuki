using System;
using Infrastructure.Tweening;
using Infrastructure.Tweening.Builders;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening.Builders
{
    public class SequenceAsyncBuilderTests
    {
        private SequenceAsyncBuilder _sequenceAsyncBuilder;

        [SetUp]
        public void SetUp()
        {
            _sequenceAsyncBuilder = new SequenceAsyncBuilder();
        }

        [Test]
        public void Build_ReturnsExpected()
        {
            const bool autoPlay = true;
            const float delayBeforeS = 1.0f;
            const float delayAfterS = 2.0f;
            const int repetitions = 1;
            const RepetitionType repetitionType = RepetitionType.Yoyo;
            const DelayManagement delayManagementRepetition = DelayManagement.Before;
            const DelayManagement delayManagementRestart = DelayManagement.After;
            Action onStep = Substitute.For<Action>();
            Action onStartIteration = Substitute.For<Action>();
            Action onStartPlay = Substitute.For<Action>();
            Action onPlay = Substitute.For<Action>();
            Action onEndPlay = Substitute.For<Action>();
            Action onEndIteration = Substitute.For<Action>();
            Action onComplete = Substitute.For<Action>();
            Action onPause = Substitute.For<Action>();
            Action onResume = Substitute.For<Action>();
            Action onRestart = Substitute.For<Action>();
            ITween expectedResult =
                new SequenceAsync(
                    autoPlay,
                    delayBeforeS,
                    delayAfterS,
                    repetitions,
                    repetitionType,
                    delayManagementRepetition,
                    delayManagementRestart,
                    onStep,
                    onStartIteration,
                    onStartPlay,
                    onPlay,
                    onEndPlay,
                    onEndIteration,
                    onComplete,
                    onPause,
                    onResume,
                    onRestart,
                    _sequenceAsyncBuilder.Tweens
                );
            _sequenceAsyncBuilder
                .WithAutoPlay(autoPlay)
                .WithDelayBeforeS(delayBeforeS)
                .WithDelayAfterS(delayAfterS)
                .WithRepetitions(repetitions)
                .WithRepetitionType(repetitionType)
                .WithDelayManagementRepetition(delayManagementRepetition)
                .WithDelayManagementRestart(delayManagementRestart)
                .WithOnStep(onStep)
                .WithOnStartIteration(onStartIteration)
                .WithOnStartPlay(onStartPlay)
                .WithOnPlay(onPlay)
                .WithOnEndPlay(onEndPlay)
                .WithOnEndIteration(onEndIteration)
                .WithOnComplete(onComplete)
                .WithOnPause(onPause)
                .WithOnResume(onResume)
                .WithOnRestart(onRestart);

            ITween result = _sequenceAsyncBuilder.Build();

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Equals_OtherNull_ReturnsFalse()
        {
            const SequenceAsyncBuilder other = null;

            Assert.IsFalse(_sequenceAsyncBuilder.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_SameRef_ReturnsTrue()
        {
            SequenceAsyncBuilder other = _sequenceAsyncBuilder;

            Assert.IsTrue(_sequenceAsyncBuilder.Equals(other)); // Assert.AreEqual cannot be used in here
        }

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            object other = new();

            Assert.AreNotEqual(_sequenceAsyncBuilder, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            SequenceAsyncBuilder other = new();

            Assert.AreEqual(_sequenceAsyncBuilder, other);
        }

        [Test]
        public void GetHashCode_OtherSameParams_SameReturnedValue()
        {
            SequenceAsyncBuilder other = new();

            Assert.AreEqual(_sequenceAsyncBuilder.GetHashCode(), other.GetHashCode());
        }
    }
}