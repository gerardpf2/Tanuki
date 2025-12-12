using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class InOutSineFunctionTests
    {
        private InOutSineFunction _inOutSineFunction;

        [SetUp]
        public void SetUp()
        {
            _inOutSineFunction = new InOutSineFunction();
        }

        [Test]
        public void Ctor_GetterIsInOutSine()
        {
            EasingFunction easingFunction = new(Easing.InOutSine);

            Assert.AreEqual(easingFunction, _inOutSineFunction);
        }
    }
}