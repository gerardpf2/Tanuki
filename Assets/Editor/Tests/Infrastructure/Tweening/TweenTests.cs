using System;
using Infrastructure.Tweening;
using Infrastructure.Tweening.EasingFunctions;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class TweenTests
    {
        private object _target;
        private object _start;
        private object _end;
        private Action<object, object> _setter;
        private IEasingFunction _easingFunction;
        private IEasingFunction _easingFunctionBackwards;
        private Func<object, object, float, object> _lerp;

        [SetUp]
        public void SetUp()
        {
            _target = new object();
            _start = new object();
            _end = new object();
            _setter = Substitute.For<Action<object, object>>();
            _easingFunction = Substitute.For<IEasingFunction>();
            _easingFunctionBackwards = Substitute.For<IEasingFunction>();
            _lerp = Substitute.For<Func<object, object, float, object>>();
        }

        [Test]
        public void Target_ITween_ReturnsExpected()
        {
            object expectedResult = new();
            ITween tween = Build(target: expectedResult);

            object result = tween.Target;

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void DurationS_ReturnsExpected()
        {
            const float expectedResult = 1.5f;
            Tween<object, object> tween = Build(durationS: expectedResult);

            float result = tween.DurationS;

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Target_ReturnsExpected()
        {
            object expectedResult = new();
            Tween<object, object> tween = Build(target: expectedResult);

            object result = tween.Target;

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void Start_ReturnsExpected()
        {
            object expectedResult = new();
            Tween<object, object> tween = Build(start: expectedResult);

            object result = tween.Start;

            Assert.AreSame(expectedResult, result);
        }

        [Test]
        public void End_ReturnsExpected()
        {
            object expectedResult = new();
            Tween<object, object> tween = Build(end: expectedResult);

            object result = tween.End;

            Assert.AreSame(expectedResult, result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Step_PlayAndPlayTimeLessThanDuration_ReturnsExpectedAndSetterCalledWithValidParams(bool backwards)
        {
            const float durationS = 1.5f;
            const float deltaTimeS = 1.0f;
            float expectedRemainingDeltaTimeS = Math.Max(deltaTimeS - durationS, 0.0f);
            const float playTimeS = deltaTimeS;
            const float normalizedTime = playTimeS / durationS;
            const float easingFunctionBackwardsEvaluateResult = 5.0f;
            const float easingFunctionEvaluateResult = 10.0f;
            object lerpResult = new();
            if (backwards)
            {
                _easingFunctionBackwards.Evaluate(normalizedTime).Returns(easingFunctionBackwardsEvaluateResult);
            }
            else
            {
                _easingFunction.Evaluate(normalizedTime).Returns(easingFunctionEvaluateResult);
            }
            _lerp.Invoke(backwards ? _end : _start, backwards ? _start : _end, backwards ? easingFunctionBackwardsEvaluateResult : easingFunctionEvaluateResult).Returns(lerpResult);
            Tween<object, object> tween = Build(durationS: durationS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration
            tween.Step(deltaTimeS); // WaitBefore
            tween.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = tween.Step(deltaTimeS, backwards);

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            _setter.Received(1).Invoke(_target, lerpResult);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Step_PlayAndPlayTimeEqualToDuration_ReturnsExpectedAndSetterCalledWithValidParams(bool backwards)
        {
            const float durationS = 1.5f;
            const float deltaTimeS = durationS;
            float expectedRemainingDeltaTimeS = Math.Max(deltaTimeS - durationS, 0.0f);
            Tween<object, object> tween = Build(durationS: durationS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration
            tween.Step(deltaTimeS); // WaitBefore
            tween.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = tween.Step(deltaTimeS, backwards);

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            _setter.Received(1).Invoke(_target, backwards ? _start : _end);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Step_PlayAndPlayTimeBiggerThanDuration_ReturnsExpectedAndSetterCalledWithValidParams(bool backwards)
        {
            const float durationS = 1.5f;
            const float deltaTimeS = 2.0f;
            float expectedRemainingDeltaTimeS = Math.Max(deltaTimeS - durationS, 0.0f);
            Tween<object, object> tween = Build(durationS: durationS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration
            tween.Step(deltaTimeS); // WaitBefore
            tween.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = tween.Step(deltaTimeS, backwards);

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            _setter.Received(1).Invoke(_target, backwards ? _start : _end);
        }

        // Equals Null / ReferenceEquals already tested

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            Tween<object, object> tween = Build();
            Tween<object, string> other = new(true, 0.0f, 0.0f, 0, RepetitionType.Restart, DelayManagement.BeforeAndAfter, DelayManagement.BeforeAndAfter, null, null, null, null, null, null, null, null, null, null, null, null, null, 1.0f, Substitute.For<Action<object, string>>(), Substitute.For<IEasingFunction>(), Substitute.For<IEasingFunction>(), Substitute.For<Func<string, string, float, string>>());

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build();

            Assert.AreEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentTarget_ReturnsFalse()
        {
            object otherTarget = new();
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build(target: otherTarget);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentStart_ReturnsFalse()
        {
            object otherStart = new();
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build(start: otherStart);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentEnd_ReturnsFalse()
        {
            object otherEnd = new();
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build(end: otherEnd);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentDurationS_ReturnsFalse()
        {
            const float durationS = 1.0f;
            const float otherDurationS = 2.0f;
            Tween<object, object> tween = Build(durationS: durationS);
            Tween<object, object> other = Build(durationS: otherDurationS);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentSetter_ReturnsFalse()
        {
            Action<object, object> otherSetter = Substitute.For<Action<object, object>>();
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build(setter: otherSetter);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentEasingFunction_ReturnsFalse()
        {
            IEasingFunction otherEasingFunction = Substitute.For<IEasingFunction>();
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build(easingFunction: otherEasingFunction);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentEasingFunctionBackwards_ReturnsFalse()
        {
            IEasingFunction otherEasingFunctionBackwards = Substitute.For<IEasingFunction>();
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build(easingFunctionBackwards: otherEasingFunctionBackwards);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentLerp_ReturnsFalse()
        {
            Func<object, object, float, object> otherLerp = Substitute.For<Func<object, object, float, object>>();
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build(lerp: otherLerp);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void GetHashCode_OtherSameParams_SameReturnedValue()
        {
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build();

            Assert.AreEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentTarget_DifferentReturnedValue()
        {
            object otherTarget = new();
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build(target: otherTarget);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentStart_DifferentReturnedValue()
        {
            object otherStart = new();
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build(start: otherStart);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentEnd_DifferentReturnedValue()
        {
            object otherEnd = new();
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build(end: otherEnd);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentDurationS_DifferentReturnedValue()
        {
            const float durationS = 1.0f;
            const float otherDurationS = 2.0f;
            Tween<object, object> tween = Build(durationS: durationS);
            Tween<object, object> other = Build(durationS: otherDurationS);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentSetter_DifferentReturnedValue()
        {
            Action<object, object> otherSetter = Substitute.For<Action<object, object>>();
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build(setter: otherSetter);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentEasingFunction_DifferentReturnedValue()
        {
            IEasingFunction otherEasingFunction = Substitute.For<IEasingFunction>();
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build(easingFunction: otherEasingFunction);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentEasingFunctionBackwards_DifferentReturnedValue()
        {
            IEasingFunction otherEasingFunctionBackwards = Substitute.For<IEasingFunction>();
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build(easingFunctionBackwards: otherEasingFunctionBackwards);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentLerp_DifferentReturnedValue()
        {
            Func<object, object, float, object> otherLerp = Substitute.For<Func<object, object, float, object>>();
            Tween<object, object> tween = Build();
            Tween<object, object> other = Build(lerp: otherLerp);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        private Tween<object, object> Build(
            object target = null,
            object start = null,
            object end = null,
            float durationS = 1.0f,
            Action<object, object> setter = null,
            IEasingFunction easingFunction = null,
            IEasingFunction easingFunctionBackwards = null,
            Func<object, object, float, object> lerp = null)
        {
            return
                new Tween<object, object>(
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
                    target ?? _target,
                    start ?? _start,
                    end ?? _end,
                    durationS,
                    setter ?? _setter,
                    easingFunction ?? _easingFunction,
                    easingFunctionBackwards ?? _easingFunctionBackwards,
                    lerp ?? _lerp
                );
        }
    }
}