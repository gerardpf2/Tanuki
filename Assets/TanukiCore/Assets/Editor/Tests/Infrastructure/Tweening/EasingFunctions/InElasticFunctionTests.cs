using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;
using UnityEngine.UIElements.Experimental;

namespace Editor.Tests.Infrastructure.Tweening.EasingFunctions
{
    public class InElasticFunctionTests
    {
        private InElasticFunction _inElasticFunction;

        [SetUp]
        public void SetUp()
        {
            _inElasticFunction = new InElasticFunction();
        }

        [Test]
        public void Ctor_GetterIsInElastic()
        {
            EasingFunction easingFunction = new(Easing.InElastic);

            Assert.AreEqual(easingFunction, _inElasticFunction);
        }
    }
}