using System;
using Infrastructure.Tweening;
using Infrastructure.Tweening.EasingFunctions;
using NSubstitute;
using NUnit.Framework;

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
        public void Get_InQuad_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInQuad(), EasingType.InQuad);
        }

        [Test]
        public void Get_OutQuad_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetOutQuad(), EasingType.OutQuad);
        }

        [Test]
        public void Get_InOutQuad_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInOutQuad(), EasingType.InOutQuad);
        }

        [Test]
        public void Get_Linear_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetLinear(), EasingType.Linear);
        }

        [Test]
        public void Get_InSine_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInSine(), EasingType.InSine);
        }

        [Test]
        public void Get_OutSine_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetOutSine(), EasingType.OutSine);
        }

        [Test]
        public void Get_InOutSine_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInOutSine(), EasingType.InOutSine);
        }

        [Test]
        public void Get_InCubic_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInCubic(), EasingType.InCubic);
        }

        [Test]
        public void Get_OutCubic_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetOutCubic(), EasingType.OutCubic);
        }

        [Test]
        public void Get_InOutCubic_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInOutCubic(), EasingType.InOutCubic);
        }

        [Test]
        public void Get_InCirc_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInCirc(), EasingType.InCirc);
        }

        [Test]
        public void Get_OutCirc_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetOutCirc(), EasingType.OutCirc);
        }

        [Test]
        public void Get_InOutCirc_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInOutCirc(), EasingType.InOutCirc);
        }

        [Test]
        public void Get_InElastic_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInElastic(), EasingType.InElastic);
        }

        [Test]
        public void Get_OutElastic_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetOutElastic(), EasingType.OutElastic);
        }

        [Test]
        public void Get_InOutElastic_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInOutElastic(), EasingType.InOutElastic);
        }

        [Test]
        public void Get_InBack_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInBack(), EasingType.InBack);
        }

        [Test]
        public void Get_OutBack_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetOutBack(), EasingType.OutBack);
        }

        [Test]
        public void Get_InOutBack_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInOutBack(), EasingType.InOutBack);
        }

        [Test]
        public void Get_InBounce_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInBounce(), EasingType.InBounce);
        }

        [Test]
        public void Get_OutBounce_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetOutBounce(), EasingType.OutBounce);
        }

        [Test]
        public void Get_InOutBounce_ReturnsExpected()
        {
            Get_ReturnsExpected(_easingFunctionFactory.GetInOutBounce(), EasingType.InOutBounce);
        }

        [Test]
        public void Get_OutOfRange_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { IEasingFunction _ = _easingFunctionGetter.Get(EasingType.InQuad - 1); });
        }

        [Test]
        public void GetComplementary_InQuad_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetOutQuad(), EasingType.InQuad);
        }

        [Test]
        public void GetComplementary_OutQuad_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInQuad(), EasingType.OutQuad);
        }

        [Test]
        public void GetComplementary_InOutQuad_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInOutQuad(), EasingType.InOutQuad);
        }

        [Test]
        public void GetComplementary_Linear_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetLinear(), EasingType.Linear);
        }

        [Test]
        public void GetComplementary_InSine_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetOutSine(), EasingType.InSine);
        }

        [Test]
        public void GetComplementary_OutSine_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInSine(), EasingType.OutSine);
        }

        [Test]
        public void GetComplementary_InOutSine_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInOutSine(), EasingType.InOutSine);
        }

        [Test]
        public void GetComplementary_InCubic_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetOutCubic(), EasingType.InCubic);
        }

        [Test]
        public void GetComplementary_OutCubic_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInCubic(), EasingType.OutCubic);
        }

        [Test]
        public void GetComplementary_InOutCubic_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInOutCubic(), EasingType.InOutCubic);
        }

        [Test]
        public void GetComplementary_InCirc_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetOutCirc(), EasingType.InCirc);
        }

        [Test]
        public void GetComplementary_OutCirc_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInCirc(), EasingType.OutCirc);
        }

        [Test]
        public void GetComplementary_InOutCirc_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInOutCirc(), EasingType.InOutCirc);
        }

        [Test]
        public void GetComplementary_InElastic_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetOutElastic(), EasingType.InElastic);
        }

        [Test]
        public void GetComplementary_OutElastic_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInElastic(), EasingType.OutElastic);
        }

        [Test]
        public void GetComplementary_InOutElastic_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInOutElastic(), EasingType.InOutElastic);
        }

        [Test]
        public void GetComplementary_InBack_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetOutBack(), EasingType.InBack);
        }

        [Test]
        public void GetComplementary_OutBack_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInBack(), EasingType.OutBack);
        }

        [Test]
        public void GetComplementary_InOutBack_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInOutBack(), EasingType.InOutBack);
        }

        [Test]
        public void GetComplementary_InBounce_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetOutBounce(), EasingType.InBounce);
        }

        [Test]
        public void GetComplementary_OutBounce_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInBounce(), EasingType.OutBounce);
        }

        [Test]
        public void GetComplementary_InOutBounce_ReturnsExpected()
        {
            GetComplementary_ReturnsExpected(_easingFunctionFactory.GetInOutBounce(), EasingType.InOutBounce);
        }

        [Test]
        public void GetComplementary_OutOfRange_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { IEasingFunction _ = _easingFunctionGetter.GetComplementary(EasingType.InQuad - 1); });
        }

        private void Get_ReturnsExpected(IEasingFunction factoryFunction, EasingType functionGetterEasingType)
        {
            IEasingFunction expectedResult = Substitute.For<IEasingFunction>();
            factoryFunction.Returns(expectedResult);

            IEasingFunction result = _easingFunctionGetter.Get(functionGetterEasingType);

            Assert.AreSame(expectedResult, result);
        }

        private void GetComplementary_ReturnsExpected(IEasingFunction factoryFunction, EasingType functionGetterEasingType)
        {
            IEasingFunction expectedResult = Substitute.For<IEasingFunction>();
            factoryFunction.Returns(expectedResult);

            IEasingFunction result = _easingFunctionGetter.GetComplementary(functionGetterEasingType);

            Assert.AreSame(expectedResult, result);
        }
    }
}