using System;
using Infrastructure.Tweening.EasingFunctions;
using NSubstitute;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class EasingFunctionTests
    {
        private Func<float, float> _getter;

        private EasingFunction _easingFunction;

        [SetUp]
        public void SetUp()
        {
            _getter = Substitute.For<Func<float, float>>();

            _easingFunction = new EasingFunction(_getter);
        }

        [Test]
        public void Evaluate_ReturnsGetterResult()
        {
            const float t = 0.0f;
            const float expectedResult = 1.0f;
            _getter.Invoke(t).Returns(expectedResult);

            float result = _easingFunction.Evaluate(t);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Equals_OtherNull_ReturnsFalse()
        {
            const EasingFunction other = null;

            Assert.IsFalse(_easingFunction.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_SameRef_ReturnsTrue()
        {
            EasingFunction other = _easingFunction;

            Assert.IsTrue(_easingFunction.Equals(other)); // Assert.AreNotEqual cannot be used in here
        }

        [Test]
        public void Equals_OtherWrongType_ReturnsFalse()
        {
            object other = new();

            Assert.AreNotEqual(_easingFunction, other);
        }

        [Test]
        public void Equals_OtherSameParams_ReturnsTrue()
        {
            EasingFunction other = new(_getter);

            Assert.AreEqual(_easingFunction, other);
        }

        [Test]
        public void Equals_OtherDifferentParams_ReturnsFalse()
        {
            Func<float, float> otherGetter = Substitute.For<Func<float, float>>();
            EasingFunction other = new(otherGetter);

            Assert.AreNotEqual(_easingFunction, other);
        }

        [Test]
        public void GetHashCode_SameParams_SameReturnedValue()
        {
            EasingFunction other = new(_getter);

            Assert.AreEqual(_easingFunction.GetHashCode(), other.GetHashCode());
        }

        [Test]
        public void GetHashCode_DifferentParams_DifferentReturnedValue()
        {
            Func<float, float> otherGetter = Substitute.For<Func<float, float>>();
            EasingFunction other = new(otherGetter);

            Assert.AreNotEqual(_easingFunction.GetHashCode(), other.GetHashCode());
        }
    }
}