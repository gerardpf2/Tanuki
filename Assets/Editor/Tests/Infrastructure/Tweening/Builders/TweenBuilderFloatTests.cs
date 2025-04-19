using System;
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
        private Action<float> _setter;

        private TweenBuilderFloat _tweenBuilderFloat;

        [SetUp]
        public void SetUp()
        {
            _easingFunctionGetter = Substitute.For<IEasingFunctionGetter>();
            _setter = Substitute.For<Action<float>>();

            _tweenBuilderFloat = new TweenBuilderFloat(_setter, _easingFunctionGetter);
        }

        [Test]
        public void Ctor_LerpIsMathfLerp()
        {
            TweenBuilder<float> tweenBuilder = new(_setter, _easingFunctionGetter, Mathf.Lerp);

            Assert.AreEqual(tweenBuilder, _tweenBuilderFloat);
        }
    }
}