using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class InCircFunctionTests
    {
        private InCircFunction _inCircFunction;

        [SetUp]
        public void SetUp()
        {
            _inCircFunction = new InCircFunction();
        }

        [Test]
        public void Ctor_GetterIsInCirc()
        {
            EasingFunction easingFunction = new(Easing.InCirc);

            Assert.AreEqual(easingFunction, _inCircFunction);
        }
    }
}