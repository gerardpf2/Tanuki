using System;
using Infrastructure.Tweening;
using Infrastructure.Tweening.EasingFunctions;
using NUnit.Framework;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class EasingFunctionFactoryTests
    {
        private EasingFunctionFactory _easingFunctionFactory;

        [SetUp]
        public void SetUp()
        {
            _easingFunctionFactory = new EasingFunctionFactory();
        }

        [Test]
        public void GetInQuad_ReturnsInQuadFunction()
        {
            Get_ReturnsExpected<InQuadFunction>(_easingFunctionFactory.GetInQuad);
        }

        [Test]
        public void GetOutQuad_ReturnsOutQuadFunction()
        {
            Get_ReturnsExpected<OutQuadFunction>(_easingFunctionFactory.GetOutQuad);
        }

        [Test]
        public void GetInOutQuad_ReturnsInOutQuadFunction()
        {
            Get_ReturnsExpected<InOutQuadFunction>(_easingFunctionFactory.GetInOutQuad);
        }

        [Test]
        public void GetLinear_ReturnsLinearFunction()
        {
            Get_ReturnsExpected<LinearFunction>(_easingFunctionFactory.GetLinear);
        }

        [Test]
        public void GetInSine_ReturnsInSineFunction()
        {
            Get_ReturnsExpected<InSineFunction>(_easingFunctionFactory.GetInSine);
        }

        [Test]
        public void GetOutSine_ReturnsOutSineFunction()
        {
            Get_ReturnsExpected<OutSineFunction>(_easingFunctionFactory.GetOutSine);
        }

        [Test]
        public void GetInOutSine_ReturnsInOutSineFunction()
        {
            Get_ReturnsExpected<InOutSineFunction>(_easingFunctionFactory.GetInOutSine);
        }

        [Test]
        public void GetInCubic_ReturnsInCubicFunction()
        {
            Get_ReturnsExpected<InCubicFunction>(_easingFunctionFactory.GetInCubic);
        }

        [Test]
        public void GetOutCubic_ReturnsOutCubicFunction()
        {
            Get_ReturnsExpected<OutCubicFunction>(_easingFunctionFactory.GetOutCubic);
        }

        [Test]
        public void GetInOutCubic_ReturnsInOutCubicFunction()
        {
            Get_ReturnsExpected<InOutCubicFunction>(_easingFunctionFactory.GetInOutCubic);
        }

        [Test]
        public void GetInCirc_ReturnsInCircFunction()
        {
            Get_ReturnsExpected<InCircFunction>(_easingFunctionFactory.GetInCirc);
        }

        [Test]
        public void GetOutCirc_ReturnsOutCircFunction()
        {
            Get_ReturnsExpected<OutCircFunction>(_easingFunctionFactory.GetOutCirc);
        }

        [Test]
        public void GetInOutCirc_ReturnsInOutCircFunction()
        {
            Get_ReturnsExpected<InOutCircFunction>(_easingFunctionFactory.GetInOutCirc);
        }

        [Test]
        public void GetInElastic_ReturnsInElasticFunction()
        {
            Get_ReturnsExpected<InElasticFunction>(_easingFunctionFactory.GetInElastic);
        }

        [Test]
        public void GetOutElastic_ReturnsOutElasticFunction()
        {
            Get_ReturnsExpected<OutElasticFunction>(_easingFunctionFactory.GetOutElastic);
        }

        [Test]
        public void GetInOutElastic_ReturnsInOutElasticFunction()
        {
            Get_ReturnsExpected<InOutElasticFunction>(_easingFunctionFactory.GetInOutElastic);
        }

        [Test]
        public void GetInBack_ReturnsInBackFunction()
        {
            Get_ReturnsExpected<InBackFunction>(_easingFunctionFactory.GetInBack);
        }

        [Test]
        public void GetOutBack_ReturnsOutBackFunction()
        {
            Get_ReturnsExpected<OutBackFunction>(_easingFunctionFactory.GetOutBack);
        }

        [Test]
        public void GetInOutBack_ReturnsInOutBackFunction()
        {
            Get_ReturnsExpected<InOutBackFunction>(_easingFunctionFactory.GetInOutBack);
        }

        [Test]
        public void GetInBounce_ReturnsInBounceFunction()
        {
            Get_ReturnsExpected<InBounceFunction>(_easingFunctionFactory.GetInBounce);
        }

        [Test]
        public void GetOutBounce_ReturnsOutBounceFunction()
        {
            Get_ReturnsExpected<OutBounceFunction>(_easingFunctionFactory.GetOutBounce);
        }

        [Test]
        public void GetInOutBounce_ReturnsInOutBounceFunction()
        {
            Get_ReturnsExpected<InOutBounceFunction>(_easingFunctionFactory.GetInOutBounce);
        }

        private static void Get_ReturnsExpected<T>(Func<IEasingFunction> factoryFunction) where T : IEasingFunction, new()
        {
            T expectedResult = new();

            IEasingFunction result = factoryFunction();

            Assert.AreEqual(expectedResult, result);
        }
    }
}