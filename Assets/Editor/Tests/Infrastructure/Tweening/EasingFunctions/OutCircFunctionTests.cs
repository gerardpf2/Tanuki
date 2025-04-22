using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class OutCircFunctionTests
    {
        private OutCircFunction _outCircFunction;

        [SetUp]
        public void SetUp()
        {
            _outCircFunction = new OutCircFunction();
        }

        [Test]
        public void Ctor_GetterIsOutCirc()
        {
            EasingFunction easingFunction = new(Easing.OutCirc);

            Assert.AreEqual(easingFunction, _outCircFunction);
        }
    }
}