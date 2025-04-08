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

        private TweenBuilderVector3 _tweenBuilderVector3;

        [SetUp]
        public void SetUp()
        {
            _easingFunctionGetter = Substitute.For<IEasingFunctionGetter>();

            _tweenBuilderVector3 = new TweenBuilderVector3(_easingFunctionGetter);
        }

        [Test]
        public void Ctor_LerpIsVector3Lerp()
        {
            TweenBuilder<Vector3> tweenBuilder = new(_easingFunctionGetter, Vector3.Lerp);

            Assert.AreEqual(tweenBuilder, _tweenBuilderVector3);
        }
    }
}