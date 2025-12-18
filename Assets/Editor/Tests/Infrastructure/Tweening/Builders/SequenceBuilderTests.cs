using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Tweening;
using Infrastructure.Tweening.Builders;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening.Builders
{
    public class SequenceBuilderTests
    {
        // It also covers SequenceBaseBuilderHelper tests

        private SequenceBuilder _sequenceBuilder;

        [SetUp]
        public void SetUp()
        {
            _sequenceBuilder = new SequenceBuilder();
        }

        #region SequenceBaseBuilderHelper

        [Test]
        public void Tweens_NotSet_ReturnsNotNullAndEmpty()
        {
            IEnumerable<ITween> result = _sequenceBuilder.Tweens;

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void Tweens_AddTween_ReturnsNotNullAndTweenIsAdded()
        {
            ITween tween = Substitute.For<ITween>();
            _sequenceBuilder.AddTween(tween);

            IEnumerable<ITween> result = _sequenceBuilder.Tweens;

            Assert.IsNotNull(result);

            List<ITween> resultList = result.ToList();

            Assert.IsTrue(resultList.Count == 1);
            Assert.IsTrue(resultList.Contains(tween));
        }

        [Test]
        public void AddTween_ReturnsThis()
        {
            ISequenceBuilder expectedResult = _sequenceBuilder;
            ITween tween = Substitute.For<ITween>();

            ISequenceBuilder result = _sequenceBuilder.AddTween(tween);

            Assert.AreSame(expectedResult, result);
        }

        #endregion

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
            Action onStartIteration = Substitute.For<Action>();
            Action onStartPlay = Substitute.For<Action>();
            Action onPlaying = Substitute.For<Action>();
            Action onEndPlay = Substitute.For<Action>();
            Action onEndIteration = Substitute.For<Action>();
            Action onPause = Substitute.For<Action>();
            Action onResume = Substitute.For<Action>();
            Action onRestart = Substitute.For<Action>();
            Action onComplete = Substitute.For<Action>();
            ITween expectedResult =
                new Sequence(
                    autoPlay,
                    delayBeforeS,
                    delayAfterS,
                    repetitions,
                    repetitionType,
                    delayManagementRepetition,
                    delayManagementRestart,
                    onStartIteration,
                    onStartPlay,
                    onPlaying,
                    onEndPlay,
                    onEndIteration,
                    onPause,
                    onResume,
                    onRestart,
                    onComplete,
                    _sequenceBuilder.Tweens
                );
            _sequenceBuilder
                .WithAutoPlay(autoPlay)
                .WithDelayBeforeS(delayBeforeS)
                .WithDelayAfterS(delayAfterS)
                .WithRepetitions(repetitions)
                .WithRepetitionType(repetitionType)
                .WithDelayManagementRepetition(delayManagementRepetition)
                .WithDelayManagementRestart(delayManagementRestart)
                .WithOnStartIteration(onStartIteration)
                .WithOnStartPlay(onStartPlay)
                .WithOnPlaying(onPlaying)
                .WithOnEndPlay(onEndPlay)
                .WithOnEndIteration(onEndIteration)
                .WithOnPause(onPause)
                .WithOnResume(onResume)
                .WithOnRestart(onRestart)
                .WithOnComplete(onComplete);

            ITween result = _sequenceBuilder.Build();

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Equals_OtherNull_ReturnsFalse()
        {
            const SequenceBuilder other = null;

            Assert.IsFalse(_sequenceBuilder.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_SameRef_ReturnsTrue()
        {
            SequenceBuilder other = _sequenceBuilder;

            Assert.IsTrue(_sequenceBuilder.Equals(other)); // Assert.AreEqual cannot be used in here
        }

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            object other = new();

            Assert.AreNotEqual(_sequenceBuilder, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            SequenceBuilder other = new();

            Assert.AreEqual(_sequenceBuilder, other);
        }

        [Test]
        public void GetHashCode_OtherSameParams_SameReturnedValue()
        {
            SequenceBuilder other = new();

            Assert.AreEqual(_sequenceBuilder.GetHashCode(), other.GetHashCode());
        }
    }
}