using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class InOutQuadFunctionTests
    {
        private InOutQuadFunction _inOutQuadFunction;

        [SetUp]
        public void SetUp()
        {
            _inOutQuadFunction = new InOutQuadFunction();
        }

        [Test]
        public void Ctor_GetterIsInOutQuad()
        {
            EasingFunction easingFunction = new(Easing.InOutQuad);

            Assert.AreEqual(easingFunction, _inOutQuadFunction);
        }
    }
}