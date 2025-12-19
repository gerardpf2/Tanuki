using System;
using Infrastructure.Tweening;
using Infrastructure.Tweening.Builders;
using Infrastructure.Tweening.EasingFunctions;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening.Builders
{
    public class TweenBuilderTests
    {
        // It also covers TweenBaseBuilderHelper tests

        private IEasingFunctionGetter _easingFunctionGetter;
        private Func<object, object, float, object> _lerp;
        private Action<object> _setter;

        private TweenBuilder<object> _tweenBuilder;

        [SetUp]
        public void SetUp()
        {
            _easingFunctionGetter = Substitute.For<IEasingFunctionGetter>();
            _lerp = Substitute.For<Func<object, object, float, object>>();
            _setter = Substitute.For<Action<object>>();

            _tweenBuilder = new TweenBuilder<object>(_setter, _easingFunctionGetter, _lerp);
        }

        #region TweenBaseBuilderHelper

        [Test]
        public void AutoPlay_NotSet_ReturnsConstant()
        {
            const bool expectedResult = TweenBaseBuilderConstants.AutoPlay;

            bool result = _tweenBuilder.AutoPlay;

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void AutoPlay_Set_ReturnsExpected(bool expectedResult)
        {
            _tweenBuilder.WithAutoPlay(expectedResult);

            bool result = _tweenBuilder.AutoPlay;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithAutoPlay_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithAutoPlay(false);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void DelayBeforeS_NotSet_ReturnsDefault()
        {
            const float expectedResult = 0.0f;

            float result = _tweenBuilder.DelayBeforeS;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void DelayBeforeS_Set_ReturnsExpected()
        {
            const float expectedResult = 1.0f;
            _tweenBuilder.WithDelayBeforeS(expectedResult);

            float result = _tweenBuilder.DelayBeforeS;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithDelayBeforeS_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithDelayBeforeS(0.0f);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void DelayAfterS_NotSet_ReturnsDefault()
        {
            const float expectedResult = 0.0f;

            float result = _tweenBuilder.DelayAfterS;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void DelayAfterS_Set_ReturnsExpected()
        {
            const float expectedResult = 1.0f;
            _tweenBuilder.WithDelayAfterS(expectedResult);

            float result = _tweenBuilder.DelayAfterS;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithDelayAfterS_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithDelayAfterS(0.0f);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void Repetitions_NotSet_ReturnsDefault()
        {
            const int expectedResult = 0;

            int result = _tweenBuilder.Repetitions;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Repetitions_Set_ReturnsExpected()
        {
            const int expectedResult = 1;
            _tweenBuilder.WithRepetitions(expectedResult);

            int result = _tweenBuilder.Repetitions;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithRepetitions_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithRepetitions(0);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void RepetitionType_NotSet_ReturnsConstant()
        {
            const RepetitionType expectedResult = TweenBaseBuilderConstants.RepetitionType;

            RepetitionType result = _tweenBuilder.RepetitionType;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void RepetitionType_Set_ReturnsExpected()
        {
            foreach (RepetitionType expectedResult in Enum.GetValues(typeof(RepetitionType)))
            {
                _tweenBuilder.WithRepetitionType(expectedResult);

                RepetitionType result = _tweenBuilder.RepetitionType;

                Assert.AreEqual(expectedResult, result);
            }
        }

        [Test]
        public void WithRepetitionType_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithRepetitionType(RepetitionType.Restart);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void DelayManagementRepetition_NotSet_ReturnsConstant()
        {
            const DelayManagement expectedResult = TweenBaseBuilderConstants.DelayManagementRepetition;

            DelayManagement result = _tweenBuilder.DelayManagementRepetition;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void DelayManagementRepetition_Set_ReturnsExpected()
        {
            foreach (DelayManagement expectedResult in Enum.GetValues(typeof(DelayManagement)))
            {
                _tweenBuilder.WithDelayManagementRepetition(expectedResult);

                DelayManagement result = _tweenBuilder.DelayManagementRepetition;

                Assert.AreEqual(expectedResult, result);
            }
        }

        [Test]
        public void WithDelayManagementRepetition_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithDelayManagementRepetition(DelayManagement.BeforeAndAfter);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void DelayManagementRestart_NotSet_ReturnsConstant()
        {
            const DelayManagement expectedResult = TweenBaseBuilderConstants.DelayManagementRestart;

            DelayManagement result = _tweenBuilder.DelayManagementRestart;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void DelayManagementRestart_Set_ReturnsExpected()
        {
            foreach (DelayManagement expectedResult in Enum.GetValues(typeof(DelayManagement)))
            {
                _tweenBuilder.WithDelayManagementRestart(expectedResult);

                DelayManagement result = _tweenBuilder.DelayManagementRestart;

                Assert.AreEqual(expectedResult, result);
            }
        }

        [Test]
        public void WithDelayManagementRestart_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithDelayManagementRestart(DelayManagement.BeforeAndAfter);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void OnStep_NotSet_ReturnsDefault()
        {
            const Action<ITween<object>> expectedResult = null;

            Action<ITween<object>> result = _tweenBuilder.OnStep;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void OnStep_Set_ReturnsExpected()
        {
            Action<ITween<object>> expectedResult = Substitute.For<Action<ITween<object>>>();
            _tweenBuilder.WithOnStep(expectedResult);

            Action<ITween<object>> result = _tweenBuilder.OnStep;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithOnStep_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithOnStep(null);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void OnStartIteration_NotSet_ReturnsDefault()
        {
            const Action<ITween<object>> expectedResult = null;

            Action<ITween<object>> result = _tweenBuilder.OnStartIteration;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void OnStartIteration_Set_ReturnsExpected()
        {
            Action<ITween<object>> expectedResult = Substitute.For<Action<ITween<object>>>();
            _tweenBuilder.WithOnStartIteration(expectedResult);

            Action<ITween<object>> result = _tweenBuilder.OnStartIteration;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithOnStartIteration_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithOnStartIteration(null);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void OnStartPlay_NotSet_ReturnsDefault()
        {
            const Action<ITween<object>> expectedResult = null;

            Action<ITween<object>> result = _tweenBuilder.OnStartPlay;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void OnStartPlay_Set_ReturnsExpected()
        {
            Action<ITween<object>> expectedResult = Substitute.For<Action<ITween<object>>>();
            _tweenBuilder.WithOnStartPlay(expectedResult);

            Action<ITween<object>> result = _tweenBuilder.OnStartPlay;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithOnStartPlay_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithOnStartPlay(null);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void OnPlay_NotSet_ReturnsDefault()
        {
            const Action<ITween<object>> expectedResult = null;

            Action<ITween<object>> result = _tweenBuilder.OnPlay;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void OnPlay_Set_ReturnsExpected()
        {
            Action<ITween<object>> expectedResult = Substitute.For<Action<ITween<object>>>();
            _tweenBuilder.WithOnPlay(expectedResult);

            Action<ITween<object>> result = _tweenBuilder.OnPlay;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithOnPlay_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithOnPlay(null);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void OnEndPlay_NotSet_ReturnsDefault()
        {
            const Action<ITween<object>> expectedResult = null;

            Action<ITween<object>> result = _tweenBuilder.OnEndPlay;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void OnEndPlay_Set_ReturnsExpected()
        {
            Action<ITween<object>> expectedResult = Substitute.For<Action<ITween<object>>>();
            _tweenBuilder.WithOnEndPlay(expectedResult);

            Action<ITween<object>> result = _tweenBuilder.OnEndPlay;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithOnEndPlay_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithOnEndPlay(null);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void OnEndIteration_NotSet_ReturnsDefault()
        {
            const Action<ITween<object>> expectedResult = null;

            Action<ITween<object>> result = _tweenBuilder.OnEndIteration;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void OnEndIteration_Set_ReturnsExpected()
        {
            Action<ITween<object>> expectedResult = Substitute.For<Action<ITween<object>>>();
            _tweenBuilder.WithOnEndIteration(expectedResult);

            Action<ITween<object>> result = _tweenBuilder.OnEndIteration;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithOnEndIteration_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithOnEndIteration(null);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void OnComplete_NotSet_ReturnsDefault()
        {
            const Action<ITween<object>> expectedResult = null;

            Action<ITween<object>> result = _tweenBuilder.OnComplete;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void OnComplete_Set_ReturnsExpected()
        {
            Action<ITween<object>> expectedResult = Substitute.For<Action<ITween<object>>>();
            _tweenBuilder.WithOnComplete(expectedResult);

            Action<ITween<object>> result = _tweenBuilder.OnComplete;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithOnComplete_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithOnComplete(null);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void OnPause_NotSet_ReturnsDefault()
        {
            const Action<ITween<object>> expectedResult = null;

            Action<ITween<object>> result = _tweenBuilder.OnPause;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void OnPause_Set_ReturnsExpected()
        {
            Action<ITween<object>> expectedResult = Substitute.For<Action<ITween<object>>>();
            _tweenBuilder.WithOnPause(expectedResult);

            Action<ITween<object>> result = _tweenBuilder.OnPause;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithOnPause_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithOnPause(null);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void OnResume_NotSet_ReturnsDefault()
        {
            const Action<ITween<object>> expectedResult = null;

            Action<ITween<object>> result = _tweenBuilder.OnResume;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void OnResume_Set_ReturnsExpected()
        {
            Action<ITween<object>> expectedResult = Substitute.For<Action<ITween<object>>>();
            _tweenBuilder.WithOnResume(expectedResult);

            Action<ITween<object>> result = _tweenBuilder.OnResume;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithOnResume_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithOnResume(null);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void OnRestart_NotSet_ReturnsDefault()
        {
            const Action<ITween<object>> expectedResult = null;

            Action<ITween<object>> result = _tweenBuilder.OnRestart;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void OnRestart_Set_ReturnsExpected()
        {
            Action<ITween<object>> expectedResult = Substitute.For<Action<ITween<object>>>();
            _tweenBuilder.WithOnRestart(expectedResult);

            Action<ITween<object>> result = _tweenBuilder.OnRestart;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithOnRestart_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithOnRestart(null);

            Assert.AreSame(expectedResult, result);
        }

        #endregion

        [Test]
        public void Start_NotSet_ReturnsDefault()
        {
            const object expectedResult = null;

            object result = _tweenBuilder.Start;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Start_Set_ReturnsExpected()
        {
            object expectedResult = new();
            _tweenBuilder.WithStart(expectedResult);

            object result = _tweenBuilder.Start;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithStart_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithStart(null);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void End_NotSet_ReturnsDefault()
        {
            const object expectedResult = null;

            object result = _tweenBuilder.End;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void End_Set_ReturnsExpected()
        {
            object expectedResult = new();
            _tweenBuilder.WithEnd(expectedResult);

            object result = _tweenBuilder.End;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithEnd_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithEnd(null);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void DurationS_NotSet_ReturnsDefault()
        {
            const float expectedResult = 0.0f;

            float result = _tweenBuilder.DurationS;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void DurationS_Set_ReturnsExpected()
        {
            const float expectedResult = 1.0f;
            _tweenBuilder.WithDurationS(expectedResult);

            float result = _tweenBuilder.DurationS;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithDurationS_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithDurationS(0.0f);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void EasingType_NotSet_ReturnsConstant()
        {
            const EasingType expectedResult = TweenBuilderConstants.EasingType;

            EasingType result = _tweenBuilder.EasingType;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void EasingType_Set_ReturnsExpected()
        {
            foreach (EasingType expectedResult in Enum.GetValues(typeof(EasingType)))
            {
                _tweenBuilder.WithEasingType(expectedResult);

                EasingType result = _tweenBuilder.EasingType;

                Assert.AreEqual(expectedResult, result);
            }
        }

        [Test]
        public void WithEasingType_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithEasingType(EasingType.InQuad);

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void ComplementaryEasingTypeBackwards_NotSet_ReturnsDefault()
        {
            const bool expectedResult = false;

            bool result = _tweenBuilder.ComplementaryEasingTypeBackwards;

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ComplementaryEasingTypeBackwards_Set_ReturnsExpected(bool expectedResult)
        {
            _tweenBuilder.WithComplementaryEasingTypeBackwards(expectedResult);

            bool result = _tweenBuilder.ComplementaryEasingTypeBackwards;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void WithComplementaryEasingTypeBackwards_ReturnsThis()
        {
            ITweenBuilder<object> expectedResult = _tweenBuilder;

            ITweenBuilder<object> result = _tweenBuilder.WithComplementaryEasingTypeBackwards(false);

            Assert.AreSame(expectedResult, result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Build_ReturnsExpected(bool complementaryEasingTypeBackwards)
        {
            const bool autoPlay = true;
            const float delayBeforeS = 1.0f;
            const float delayAfterS = 2.0f;
            const int repetitions = 1;
            const RepetitionType repetitionType = RepetitionType.Yoyo;
            const DelayManagement delayManagementRepetition = DelayManagement.Before;
            const DelayManagement delayManagementRestart = DelayManagement.After;
            Action<ITween<object>> onStep = Substitute.For<Action<ITween<object>>>();
            Action<ITween<object>> onStartIteration = Substitute.For<Action<ITween<object>>>();
            Action<ITween<object>> onStartPlay = Substitute.For<Action<ITween<object>>>();
            Action<ITween<object>> onPlay = Substitute.For<Action<ITween<object>>>();
            Action<ITween<object>> onEndPlay = Substitute.For<Action<ITween<object>>>();
            Action<ITween<object>> onEndIteration = Substitute.For<Action<ITween<object>>>();
            Action<ITween<object>> onComplete = Substitute.For<Action<ITween<object>>>();
            Action<ITween<object>> onPause = Substitute.For<Action<ITween<object>>>();
            Action<ITween<object>> onResume = Substitute.For<Action<ITween<object>>>();
            Action<ITween<object>> onRestart = Substitute.For<Action<ITween<object>>>();
            object start = new();
            object end = new();
            const float durationS = 1.0f;
            const EasingType easingType = EasingType.InCubic;
            IEasingFunction easingFunction = Substitute.For<IEasingFunction>();
            IEasingFunction easingFunctionBackwards = Substitute.For<IEasingFunction>();
            _easingFunctionGetter.Get(easingType).Returns(easingFunction);
            _easingFunctionGetter.GetComplementary(easingType).Returns(easingFunctionBackwards);
            ITweenBase expectedResult =
                new Tween<object>(
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
                    start,
                    end,
                    durationS,
                    _setter,
                    easingFunction,
                    complementaryEasingTypeBackwards ? easingFunctionBackwards : easingFunction,
                    _lerp
                );
            _tweenBuilder
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
                .WithOnRestart(onRestart)
                .WithStart(start)
                .WithEnd(end)
                .WithDurationS(durationS)
                .WithEasingType(easingType)
                .WithComplementaryEasingTypeBackwards(complementaryEasingTypeBackwards);

            ITweenBase result = _tweenBuilder.Build();

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Build_AlreadyBuilt_ThrowsException()
        {
            ITweenBase _ = _tweenBuilder.Build();

            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => { ITweenBase __ = _tweenBuilder.Build(); });
            Assert.AreEqual("Tween has already been built. Tween builders are not expected to be reused", invalidOperationException.Message);
        }

        [Test]
        public void Equals_OtherNull_ReturnsFalse()
        {
            const TweenBuilder<object> other = null;

            Assert.IsFalse(_tweenBuilder.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_SameRef_ReturnsTrue()
        {
            TweenBuilder<object> other = _tweenBuilder;

            Assert.IsTrue(_tweenBuilder.Equals(other)); // Assert.AreEqual cannot be used in here
        }

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            object other = new();

            Assert.AreNotEqual(_tweenBuilder, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            TweenBuilder<object> other = new(_setter, _easingFunctionGetter, _lerp);

            Assert.AreEqual(_tweenBuilder, other);
        }

        [Test]
        public void Equals_OtherDifferentParams1_ReturnsFalse()
        {
            Action<object> otherSetter = Substitute.For<Action<object>>();
            TweenBuilder<object> other = new(otherSetter, _easingFunctionGetter, _lerp);

            Assert.AreNotEqual(_tweenBuilder, other);
        }

        [Test]
        public void Equals_OtherDifferentParams2_ReturnsFalse()
        {
            IEasingFunctionGetter otherEasingFunctionGetter = Substitute.For<IEasingFunctionGetter>();
            TweenBuilder<object> other = new(_setter, otherEasingFunctionGetter, _lerp);

            Assert.AreNotEqual(_tweenBuilder, other);
        }

        [Test]
        public void Equals_OtherDifferentParams3_ReturnsFalse()
        {
            Func<object, object, float, object> otherLerp = Substitute.For<Func<object, object, float, object>>();
            TweenBuilder<object> other = new(_setter, _easingFunctionGetter, otherLerp);

            Assert.AreNotEqual(_tweenBuilder, other);
        }

        [Test]
        public void GetHashCode_OtherSameParams_SameReturnedValue()
        {
            TweenBuilder<object> other = new(_setter, _easingFunctionGetter, _lerp);

            Assert.AreEqual(_tweenBuilder.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams1_DifferentReturnedValue()
        {
            Action<object> otherSetter = Substitute.For<Action<object>>();
            TweenBuilder<object> other = new(otherSetter, _easingFunctionGetter, _lerp);

            Assert.AreNotEqual(_tweenBuilder.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams2_DifferentReturnedValue()
        {
            IEasingFunctionGetter otherEasingFunctionGetter = Substitute.For<IEasingFunctionGetter>();
            TweenBuilder<object> other = new(_setter, otherEasingFunctionGetter, _lerp);

            Assert.AreNotEqual(_tweenBuilder.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams3_DifferentReturnedValue()
        {
            Func<object, object, float, object> otherLerp = Substitute.For<Func<object, object, float, object>>();
            TweenBuilder<object> other = new(_setter, _easingFunctionGetter, otherLerp);

            Assert.AreNotEqual(_tweenBuilder.GetHashCode(), other.GetHashCode());
        }
    }
}