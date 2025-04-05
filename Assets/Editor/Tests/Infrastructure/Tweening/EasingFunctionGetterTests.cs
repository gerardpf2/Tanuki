using System;
using Infrastructure.Tweening;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.UIElements;

namespace Editor.Tests.Infrastructure.Tweening
{
    public class EasingFunctionGetterTests
    {
        private IEasingFunctionFactory _easingFunctionFactory;

        private EasingFunctionGetter _easingFunctionGetter;

        [SetUp]
        public void SetUp()
        {
            _easingFunctionFactory = Substitute.For<IEasingFunctionFactory>();

            _easingFunctionGetter = new EasingFunctionGetter(_easingFunctionFactory);
        }

        [Test]
        public void Get_EaseIn_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInQuad(), EasingMode.EaseIn);
        }

        [Test]
        public void Get_EaseOut_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetOutQuad(), EasingMode.EaseOut);
        }

        [Test]
        public void Get_EaseInOut_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInOutQuad(), EasingMode.EaseInOut);
        }

        [Test]
        public void Get_Linear_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetLinear(), EasingMode.Linear);
        }

        [Test]
        public void Get_EaseInSine_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInSine(), EasingMode.EaseInSine);
        }

        [Test]
        public void Get_EaseOutSine_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetOutSine(), EasingMode.EaseOutSine);
        }

        [Test]
        public void Get_EaseInOutSine_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInOutSine(), EasingMode.EaseInOutSine);
        }

        [Test]
        public void Get_EaseInCubic_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInCubic(), EasingMode.EaseInCubic);
        }

        [Test]
        public void Get_EaseOutCubic_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetOutCubic(), EasingMode.EaseOutCubic);
        }

        [Test]
        public void Get_EaseInOutCubic_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInOutCubic(), EasingMode.EaseInOutCubic);
        }

        [Test]
        public void Get_EaseInCirc_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInCirc(), EasingMode.EaseInCirc);
        }

        [Test]
        public void Get_EaseOutCirc_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetOutCirc(), EasingMode.EaseOutCirc);
        }

        [Test]
        public void Get_EaseInOutCirc_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInOutCirc(), EasingMode.EaseInOutCirc);
        }

        [Test]
        public void Get_EaseInElastic_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInElastic(), EasingMode.EaseInElastic);
        }

        [Test]
        public void Get_EaseOutElastic_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetOutElastic(), EasingMode.EaseOutElastic);
        }

        [Test]
        public void Get_EaseInOutElastic_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInOutElastic(), EasingMode.EaseInOutElastic);
        }

        [Test]
        public void Get_EaseInBack_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInBack(), EasingMode.EaseInBack);
        }

        [Test]
        public void Get_EaseOutBack_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetOutBack(), EasingMode.EaseOutBack);
        }

        [Test]
        public void Get_EaseInOutBack_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInOutBack(), EasingMode.EaseInOutBack);
        }

        [Test]
        public void Get_EaseInBounce_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInBounce(), EasingMode.EaseInBounce);
        }

        [Test]
        public void Get_EaseOutBounce_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetOutBounce(), EasingMode.EaseOutBounce);
        }

        [Test]
        public void Get_EaseInOutBounce_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInOutBounce(), EasingMode.EaseInOutBounce);
        }

        [Test]
        public void Get_Ease_ThrowsException()
        {
            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => { Func<float, float> _ = _easingFunctionGetter.Get(EasingMode.Ease); });
            Assert.AreEqual($"Cannot get easing function for {EasingMode.Ease}", invalidOperationException.Message);
        }

        [Test]
        public void Get_OutOfRange_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { Func<float, float> _ = _easingFunctionGetter.Get(EasingMode.Ease - 1); });
        }

        [Test]
        public void GetComplementary_EaseIn_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetOutQuad(), EasingMode.EaseIn);
        }

        [Test]
        public void GetComplementary_EaseOut_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInQuad(), EasingMode.EaseOut);
        }

        [Test]
        public void GetComplementary_EaseInOut_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInOutQuad(), EasingMode.EaseInOut);
        }

        [Test]
        public void GetComplementary_Linear_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetLinear(), EasingMode.Linear);
        }

        [Test]
        public void GetComplementary_EaseInSine_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetOutSine(), EasingMode.EaseInSine);
        }

        [Test]
        public void GetComplementary_EaseOutSine_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInSine(), EasingMode.EaseOutSine);
        }

        [Test]
        public void GetComplementary_EaseInOutSine_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInOutSine(), EasingMode.EaseInOutSine);
        }

        [Test]
        public void GetComplementary_EaseInCubic_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetOutCubic(), EasingMode.EaseInCubic);
        }

        [Test]
        public void GetComplementary_EaseOutCubic_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInCubic(), EasingMode.EaseOutCubic);
        }

        [Test]
        public void GetComplementary_EaseInOutCubic_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInOutCubic(), EasingMode.EaseInOutCubic);
        }

        [Test]
        public void GetComplementary_EaseInCirc_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetOutCirc(), EasingMode.EaseInCirc);
        }

        [Test]
        public void GetComplementary_EaseOutCirc_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInCirc(), EasingMode.EaseOutCirc);
        }

        [Test]
        public void GetComplementary_EaseInOutCirc_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInOutCirc(), EasingMode.EaseInOutCirc);
        }

        [Test]
        public void GetComplementary_EaseInElastic_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetOutElastic(), EasingMode.EaseInElastic);
        }

        [Test]
        public void GetComplementary_EaseOutElastic_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInElastic(), EasingMode.EaseOutElastic);
        }

        [Test]
        public void GetComplementary_EaseInOutElastic_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInOutElastic(), EasingMode.EaseInOutElastic);
        }

        [Test]
        public void GetComplementary_EaseInBack_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetOutBack(), EasingMode.EaseInBack);
        }

        [Test]
        public void GetComplementary_EaseOutBack_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInBack(), EasingMode.EaseOutBack);
        }

        [Test]
        public void GetComplementary_EaseInOutBack_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInOutBack(), EasingMode.EaseInOutBack);
        }

        [Test]
        public void GetComplementary_EaseInBounce_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetOutBounce(), EasingMode.EaseInBounce);
        }

        [Test]
        public void GetComplementary_EaseOutBounce_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInBounce(), EasingMode.EaseOutBounce);
        }

        [Test]
        public void GetComplementary_EaseInOutBounce_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInOutBounce(), EasingMode.EaseInOutBounce);
        }

        [Test]
        public void GetComplementary_Ease_ThrowsException()
        {
            InvalidOperationException invalidOperationException = Assert.Throws<InvalidOperationException>(() => { Func<float, float> _ = _easingFunctionGetter.GetComplementary(EasingMode.Ease); });
            Assert.AreEqual($"Cannot get complementary easing mode for {EasingMode.Ease}", invalidOperationException.Message);
        }

        [Test]
        public void GetComplementary_OutOfRange_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { Func<float, float> _ = _easingFunctionGetter.GetComplementary(EasingMode.Ease - 1); });
        }

        private void Get_ReturnsExpected(Func<float, float> factoryFunction, EasingMode functionGetterEasingMode)
        {
            Func<float, float> expectedResult = Substitute.For<Func<float, float>>();
            factoryFunction.Returns(expectedResult);

            Func<float, float> result = _easingFunctionGetter.Get(functionGetterEasingMode);

            Assert.AreSame(expectedResult, result);
        }

        private void GetComplementary_ReturnsExpected(Func<float, float> factoryFunction, EasingMode functionGetterEasingMode)
        {
            Func<float, float> expectedResult = Substitute.For<Func<float, float>>();
            factoryFunction.Returns(expectedResult);

            Func<float, float> result = _easingFunctionGetter.GetComplementary(functionGetterEasingMode);

            Assert.AreSame(expectedResult, result);
        }
    }
}