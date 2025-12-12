using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class OutSineFunctionTests
    {
        private OutSineFunction _outSineFunction;

        [SetUp]
        public void SetUp()
        {
            _outSineFunction = new OutSineFunction();
        }

        [Test]
        public void Ctor_GetterIsOutSine()
        {
            EasingFunction easingFunction = new(Easing.OutSine);

            Assert.AreEqual(easingFunction, _outSineFunction);
        }
    }
}