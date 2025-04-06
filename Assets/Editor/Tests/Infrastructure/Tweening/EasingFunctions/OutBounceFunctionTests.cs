using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class OutBounceFunctionTests
    {
        private OutBounceFunction _outBounceFunction;

        [SetUp]
        public void SetUp()
        {
            _outBounceFunction = new OutBounceFunction();
        }

        [Test]
        public void Ctor_GetterIsOutBounce()
        {
            EasingFunction easingFunction = new(Easing.OutBounce);

            Assert.AreEqual(easingFunction, _outBounceFunction);
        }
    }
}