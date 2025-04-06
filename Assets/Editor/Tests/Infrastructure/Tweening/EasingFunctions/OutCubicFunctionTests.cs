using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class OutCubicFunctionTests
    {
        private OutCubicFunction _outCubicFunction;

        [SetUp]
        public void SetUp()
        {
            _outCubicFunction = new OutCubicFunction();
        }

        [Test]
        public void Ctor_GetterIsOutCubic()
        {
            EasingFunction easingFunction = new(Easing.OutCubic);

            Assert.AreEqual(easingFunction, _outCubicFunction);
        }
    }
}