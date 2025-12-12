using System;
using Infrastructure.Tweening;
using Infrastructure.Tweening.Builders;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests.Infrastructure.Tweening.Builders
{
    public class TweenBuilderVector3Tests
    {
        private IEasingFunctionGetter _easingFunctionGetter;
        private Action<Vector3> _setter;

        private TweenBuilderVector3 _tweenBuilderVector3;

        [SetUp]
        public void SetUp()
        {
            _easingFunctionGetter = Substitute.For<IEasingFunctionGetter>();
            _setter = Substitute.For<Action<Vector3>>();

            _tweenBuilderVector3 = new TweenBuilderVector3(_setter, _easingFunctionGetter);
        }

        [Test]
        public void Ctor_LerpIsVector3Lerp()
        {
            TweenBuilder<Vector3> tweenBuilder = new(_setter, _easingFunctionGetter, Vector3.Lerp);

            Assert.AreEqual(tweenBuilder, _tweenBuilderVector3);
        }
    }
}