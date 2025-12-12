using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class InOutBackFunctionTests
    {
        private InOutBackFunction _inOutBackFunction;

        [SetUp]
        public void SetUp()
        {
            _inOutBackFunction = new InOutBackFunction();
        }

        [Test]
        public void Ctor_GetterIsInOutBack()
        {
            EasingFunction easingFunction = new(Easing.InOutBack);

            Assert.AreEqual(easingFunction, _inOutBackFunction);
        }
    }
}