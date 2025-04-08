using Infrastructure.Tweening;
using Infrastructure.Tweening.Builders;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests.Infrastructure.Tweening.Builders
{
    public class TweenBuilderFloatTests
    {
        private IEasingFunctionGetter _easingFunctionGetter;

        private TweenBuilderFloat _tweenBuilderFloat;

        [SetUp]
        public void SetUp()
        {
            _easingFunctionGetter = Substitute.For<IEasingFunctionGetter>();

            _tweenBuilderFloat = new TweenBuilderFloat(_easingFunctionGetter);
        }

        [Test]
        public void Ctor_LerpIsMathfLerp()
        {
            TweenBuilder<float> tweenBuilder = new(_easingFunctionGetter, Mathf.Lerp);

            Assert.AreEqual(tweenBuilder, _tweenBuilderFloat);
        }
    }
}