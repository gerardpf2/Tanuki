using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class InOutElasticFunctionTests
    {
        private InOutElasticFunction _inOutElasticFunction;

        [SetUp]
        public void SetUp()
        {
            _inOutElasticFunction = new InOutElasticFunction();
        }

        [Test]
        public void Ctor_GetterIsInOutElastic()
        {
            EasingFunction easingFunction = new(Easing.InOutElastic);

            Assert.AreEqual(easingFunction, _inOutElasticFunction);
        }
    }
}