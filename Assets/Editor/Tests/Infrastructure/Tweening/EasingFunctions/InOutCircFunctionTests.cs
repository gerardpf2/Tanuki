using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class InOutCircFunctionTests
    {
        private InOutCircFunction _inOutCircFunction;

        [SetUp]
        public void SetUp()
        {
            _inOutCircFunction = new InOutCircFunction();
        }

        [Test]
        public void Ctor_GetterIsInOutCirc()
        {
            EasingFunction easingFunction = new(Easing.InOutCirc);

            Assert.AreEqual(easingFunction, _inOutCircFunction);
        }
    }
}