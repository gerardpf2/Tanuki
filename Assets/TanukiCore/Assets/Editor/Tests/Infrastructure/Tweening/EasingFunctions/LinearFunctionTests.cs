using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class LinearFunctionTests
    {
        private LinearFunction _linearFunction;

        [SetUp]
        public void SetUp()
        {
            _linearFunction = new LinearFunction();
        }

        [Test]
        public void Ctor_GetterIsLinear()
        {
            EasingFunction easingFunction = new(Easing.Linear);

            Assert.AreEqual(easingFunction, _linearFunction);
        }
    }
}