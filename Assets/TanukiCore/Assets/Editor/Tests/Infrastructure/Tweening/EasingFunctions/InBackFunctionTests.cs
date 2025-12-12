using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class InBackFunctionTests
    {
        private InBackFunction _inBackFunction;

        [SetUp]
        public void SetUp()
        {
            _inBackFunction = new InBackFunction();
        }

        [Test]
        public void Ctor_GetterIsInBack()
        {
            EasingFunction easingFunction = new(Easing.InBack);

            Assert.AreEqual(easingFunction, _inBackFunction);
        }
    }
}