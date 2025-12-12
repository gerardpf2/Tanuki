using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class InOutCubicFunctionTests
    {
        private InOutCubicFunction _inOutCubicFunction;

        [SetUp]
        public void SetUp()
        {
            _inOutCubicFunction = new InOutCubicFunction();
        }

        [Test]
        public void Ctor_GetterIsInOutCubic()
        {
            EasingFunction easingFunction = new(Easing.InOutCubic);

            Assert.AreEqual(easingFunction, _inOutCubicFunction);
        }
    }
}