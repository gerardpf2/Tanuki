using System;
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
            Action<float> setter = Substitute.For<Action<float>>();
            TweenBuilderFloat expectedResult = new(setter, _easingFunctionGetter);

            ITweenBuilder<float> result = _tweenBuilderFactory.GetTweenBuilderFloat(setter);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetTweenBuilderVector3_ReturnsTweenBuilderVector3WithValidParams()
        {
            Action<Vector3> setter = Substitute.For<Action<Vector3>>();
            TweenBuilderVector3 expectedResult = new(setter, _easingFunctionGetter);

            ITweenBuilder<Vector3> result = _tweenBuilderFactory.GetTweenBuilderVector3(setter);

            Assert.AreEqual(expectedResult, result);
        }
    }
}