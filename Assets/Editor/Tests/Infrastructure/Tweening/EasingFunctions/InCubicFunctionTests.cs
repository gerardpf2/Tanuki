using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class InCubicFunctionTests
    {
        private InCubicFunction _inCubicFunction;

        [SetUp]
        public void SetUp()
        {
            _inCubicFunction = new InCubicFunction();
        }

        [Test]
        public void Ctor_GetterIsInCubic()
        {
            EasingFunction easingFunction = new(Easing.InCubic);

            Assert.AreEqual(easingFunction, _inCubicFunction);
        }
    }
}