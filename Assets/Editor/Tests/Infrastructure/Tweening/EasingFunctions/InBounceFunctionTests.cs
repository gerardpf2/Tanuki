using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class InBounceFunctionTests
    {
        private InBounceFunction _inBounceFunction;

        [SetUp]
        public void SetUp()
        {
            _inBounceFunction = new InBounceFunction();
        }

        [Test]
        public void Ctor_GetterIsInBounce()
        {
            EasingFunction easingFunction = new(Easing.InBounce);

            Assert.AreEqual(easingFunction, _inBounceFunction);
        }
    }
}