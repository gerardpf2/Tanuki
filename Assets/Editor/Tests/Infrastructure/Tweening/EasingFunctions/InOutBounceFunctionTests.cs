using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class InOutBounceFunctionTests
    {
        private InOutBounceFunction _inOutBounceFunction;

        [SetUp]
        public void SetUp()
        {
            _inOutBounceFunction = new InOutBounceFunction();
        }

        [Test]
        public void Ctor_GetterIsInOutBounce()
        {
            EasingFunction easingFunction = new(Easing.InOutBounce);

            Assert.AreEqual(easingFunction, _inOutBounceFunction);
        }
    }
}