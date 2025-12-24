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
        private Action<object, float> _setter;

        private TweenBuilderFloat<object> _tweenBuilderFloat;

        [SetUp]
        public void SetUp()
        {
            _easingFunctionGetter = Substitute.For<IEasingFunctionGetter>();
            _setter = Substitute.For<Action<object, float>>();

            _tweenBuilderFloat = new TweenBuilderFloat<object>(_setter, _easingFunctionGetter);
        }

        [Test]
        public void Ctor_LerpIsMathfLerp()
        {
            TweenBuilder<object, float> tweenBuilder = new(_setter, _easingFunctionGetter, Mathf.Lerp);

            Assert.AreEqual(tweenBuilder, _tweenBuilderFloat);
        }
    }
}