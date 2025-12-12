using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class OutBackFunctionTests
    {
        private OutBackFunction _outBackFunction;

        [SetUp]
        public void SetUp()
        {
            _outBackFunction = new OutBackFunction();
        }

        [Test]
        public void Ctor_GetterIsOutBack()
        {
            EasingFunction easingFunction = new(Easing.OutBack);

            Assert.AreEqual(easingFunction, _outBackFunction);
        }
    }
}