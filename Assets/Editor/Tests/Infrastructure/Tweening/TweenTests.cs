using System;
using Infrastructure.Tweening;
using Infrastructure.Tweening.EasingFunctions;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class TweenTests
    {
        private object _start;
        private object _end;
        private Action<object> _setter;
        private IEasingFunction _easingFunction;
        private IEasingFunction _easingFunctionBackwards;
        private Func<object, object, float, object> _lerp;

        [SetUp]
        public void SetUp()
        {
            _start = new object();
            _end = new object();
            _setter = Substitute.For<Action<object>>();
            _easingFunction = Substitute.For<IEasingFunction>();
            _easingFunctionBackwards = Substitute.For<IEasingFunction>();
            _lerp = Substitute.For<Func<object, object, float, object>>();
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
            Tween<object> tween = Build(durationS: durationS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration
            tween.Step(deltaTimeS); // WaitBefore
            tween.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = tween.Step(deltaTimeS, backwards);

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            _setter.Received(1).Invoke(lerpResult);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Step_PlayAndPlayTimeEqualToDuration_ReturnsExpectedAndSetterCalledWithValidParams(bool backwards)
        {
            const float durationS = 1.5f;
            const float deltaTimeS = durationS;
            float expectedRemainingDeltaTimeS = Math.Max(deltaTimeS - durationS, 0.0f);
            Tween<object> tween = Build(durationS: durationS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration
            tween.Step(deltaTimeS); // WaitBefore
            tween.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = tween.Step(deltaTimeS, backwards);

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            _setter.Received(1).Invoke(backwards ? _start : _end);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Step_PlayAndPlayTimeBiggerThanDuration_ReturnsExpectedAndSetterCalledWithValidParams(bool backwards)
        {
            const float durationS = 1.5f;
            const float deltaTimeS = 2.0f;
            float expectedRemainingDeltaTimeS = Math.Max(deltaTimeS - durationS, 0.0f);
            Tween<object> tween = Build(durationS: durationS);
            tween.Step(deltaTimeS); // SetUp
            tween.Step(deltaTimeS); // StartIteration
            tween.Step(deltaTimeS); // WaitBefore
            tween.Step(deltaTimeS); // StartPlay

            float remainingDeltaTimeS = tween.Step(deltaTimeS, backwards);

            Assert.AreEqual(expectedRemainingDeltaTimeS, remainingDeltaTimeS);
            _setter.Received(1).Invoke(backwards ? _start : _end);
        }

        // Equals Null / ReferenceEquals already tested

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            Tween<object> tween = Build();
            Tween<string> other = new(true, 0.0f, 0.0f, 0, RepetitionType.Restart, DelayManagement.BeforeAndAfter, DelayManagement.BeforeAndAfter, null, null, null, null, null, null, null, null, null, null, null, null, 1.0f, Substitute.For<Action<string>>(), Substitute.For<IEasingFunction>(), Substitute.For<IEasingFunction>(), Substitute.For<Func<string, string, float, string>>());

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            Tween<object> tween = Build();
            Tween<object> other = Build();

            Assert.AreEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentParams1_ReturnsFalse()
        {
            object otherStart = new();
            Tween<object> tween = Build();
            Tween<object> other = Build(start: otherStart);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentParams2_ReturnsFalse()
        {
            object otherEnd = new();
            Tween<object> tween = Build();
            Tween<object> other = Build(end: otherEnd);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentParams3_ReturnsFalse()
        {
            const float durationS = 1.0f;
            const float otherDurationS = 2.0f;
            Tween<object> tween = Build(durationS: durationS);
            Tween<object> other = Build(durationS: otherDurationS);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentParams4_ReturnsFalse()
        {
            Action<object> otherSetter = Substitute.For<Action<object>>();
            Tween<object> tween = Build();
            Tween<object> other = Build(setter: otherSetter);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentParams5_ReturnsFalse()
        {
            IEasingFunction otherEasingFunction = Substitute.For<IEasingFunction>();
            Tween<object> tween = Build();
            Tween<object> other = Build(easingFunction: otherEasingFunction);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentParams6_ReturnsFalse()
        {
            IEasingFunction otherEasingFunctionBackwards = Substitute.For<IEasingFunction>();
            Tween<object> tween = Build();
            Tween<object> other = Build(easingFunctionBackwards: otherEasingFunctionBackwards);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void Equals_OtherDifferentParams7_ReturnsFalse()
        {
            Func<object, object, float, object> otherLerp = Substitute.For<Func<object, object, float, object>>();
            Tween<object> tween = Build();
            Tween<object> other = Build(lerp: otherLerp);

            Assert.AreNotEqual(tween, other);
        }

        [Test]
        public void GetHashCode_OtherSameParams_SameReturnedValue()
        {
            Tween<object> tween = Build();
            Tween<object> other = Build();

            Assert.AreEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams1_DifferentReturnedValue()
        {
            object otherStart = new();
            Tween<object> tween = Build();
            Tween<object> other = Build(start: otherStart);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams2_DifferentReturnedValue()
        {
            object otherEnd = new();
            Tween<object> tween = Build();
            Tween<object> other = Build(end: otherEnd);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams3_DifferentReturnedValue()
        {
            const float durationS = 1.0f;
            const float otherDurationS = 2.0f;
            Tween<object> tween = Build(durationS: durationS);
            Tween<object> other = Build(durationS: otherDurationS);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams4_DifferentReturnedValue()
        {
            Action<object> otherSetter = Substitute.For<Action<object>>();
            Tween<object> tween = Build();
            Tween<object> other = Build(setter: otherSetter);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams5_DifferentReturnedValue()
        {
            IEasingFunction otherEasingFunction = Substitute.For<IEasingFunction>();
            Tween<object> tween = Build();
            Tween<object> other = Build(easingFunction: otherEasingFunction);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams6_DifferentReturnedValue()
        {
            IEasingFunction otherEasingFunctionBackwards = Substitute.For<IEasingFunction>();
            Tween<object> tween = Build();
            Tween<object> other = Build(easingFunctionBackwards: otherEasingFunctionBackwards);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_OtherDifferentParams7_DifferentReturnedValue()
        {
            Func<object, object, float, object> otherLerp = Substitute.For<Func<object, object, float, object>>();
            Tween<object> tween = Build();
            Tween<object> other = Build(lerp: otherLerp);

            Assert.AreNotEqual(tween.GetHashCode(), other.GetHashCode());
        }

        private Tween<object> Build(
            object start = null,
            object end = null,
            float durationS = 1.0f,
            Action<object> setter = null,
            IEasingFunction easingFunction = null,
            IEasingFunction easingFunctionBackwards = null,
            Func<object, object, float, object> lerp = null)
        {
            return
                new Tween<object>(
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