using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class OutElasticFunctionTests
    {
        private OutElasticFunction _outElasticFunction;

        [SetUp]
        public void SetUp()
        {
            _outElasticFunction = new OutElasticFunction();
        }

        [Test]
        public void Ctor_GetterIsOutElastic()
        {
            EasingFunction easingFunction = new(Easing.OutElastic);

            Assert.AreEqual(easingFunction, _outElasticFunction);
        }
    }
}