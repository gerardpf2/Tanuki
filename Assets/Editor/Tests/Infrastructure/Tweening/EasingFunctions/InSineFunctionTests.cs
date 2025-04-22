using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class InSineFunctionTests
    {
        private InSineFunction _inSineFunction;

        [SetUp]
        public void SetUp()
        {
            _inSineFunction = new InSineFunction();
        }

        [Test]
        public void Ctor_GetterIsInSine()
        {
            EasingFunction easingFunction = new(Easing.InSine);

            Assert.AreEqual(easingFunction, _inSineFunction);
        }
    }
}