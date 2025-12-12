using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class InQuadFunctionTests
    {
        private InQuadFunction _inQuadFunction;

        [SetUp]
        public void SetUp()
        {
            _inQuadFunction = new InQuadFunction();
        }

        [Test]
        public void Ctor_GetterIsInQuad()
        {
            EasingFunction easingFunction = new(Easing.InQuad);

            Assert.AreEqual(easingFunction, _inQuadFunction);
        }
    }
}