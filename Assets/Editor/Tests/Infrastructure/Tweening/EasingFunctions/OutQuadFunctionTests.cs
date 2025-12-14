using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class OutQuadFunctionTests
    {
        private OutQuadFunction _outQuadFunction;

        [SetUp]
        public void SetUp()
        {
            _outQuadFunction = new OutQuadFunction();
        }

        [Test]
        public void Ctor_GetterIsOutQuad()
        {
            EasingFunction easingFunction = new(Easing.OutQuad);

            Assert.AreEqual(easingFunction, _outQuadFunction);
        }
    }
}