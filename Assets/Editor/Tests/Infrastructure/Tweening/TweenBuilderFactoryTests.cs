using Infrastructure.Tweening;
using Infrastructure.Tweening.Builders;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class TweenBuilderFactoryTests
    {
        private IEasingFunctionGetter _easingFunctionGetter;

        private TweenBuilderFactory _tweenBuilderFactory;

        [SetUp]
        public void SetUp()
        {
            _easingFunctionGetter = Substitute.For<IEasingFunctionGetter>();

            _tweenBuilderFactory = new TweenBuilderFactory(_easingFunctionGetter);
        }

        [Test]
        public void GetSequenceAsyncBuilder_ReturnsSequenceAsyncBuilder()
        {
            SequenceAsyncBuilder expectedResult = new();

            ISequenceAsyncBuilder result = _tweenBuilderFactory.GetSequenceAsyncBuilder();

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetSequenceBuilder_ReturnsSequenceBuilder()
        {
            SequenceBuilder expectedResult = new();

            ISequenceBuilder result = _tweenBuilderFactory.GetSequenceBuilder();

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetTweenBuilderFloat_ReturnsTweenBuilderFloatWithValidParams()
        {
            TweenBuilderFloat expectedResult = new(_easingFunctionGetter);

            ITweenBuilder<float> result = _tweenBuilderFactory.GetTweenBuilderFloat();

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetTweenBuilderVector3_ReturnsTweenBuilderVector3WithValidParams()
        {
            TweenBuilderVector3 expectedResult = new(_easingFunctionGetter);

            ITweenBuilder<Vector3> result = _tweenBuilderFactory.GetTweenBuilderVector3();

            Assert.AreEqual(expectedResult, result);
        }
    }
}