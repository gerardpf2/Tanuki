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
            Action<object, float> setter = Substitute.For<Action<object, float>>();
            TweenBuilderFloat<object> expectedResult = new(setter, _easingFunctionGetter);

            ITweenBuilder<object, float> result = _tweenBuilderFactory.GetTweenBuilderFloat(setter);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetTweenBuilderVector3_ReturnsTweenBuilderVector3WithValidParams()
        {
            Action<object, Vector3> setter = Substitute.For<Action<object, Vector3>>();
            TweenBuilderVector3<object> expectedResult = new(setter, _easingFunctionGetter);

            ITweenBuilder<object, Vector3> result = _tweenBuilderFactory.GetTweenBuilderVector3(setter);

            Assert.AreEqual(expectedResult, result);
        }
    }
}