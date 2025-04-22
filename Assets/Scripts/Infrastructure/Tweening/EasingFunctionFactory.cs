using Infrastructure.Tweening.EasingFunctions;

namespace Infrastructure.Tweening
{
    public class EasingFunctionFactory : IEasingFunctionFactory
    {
        public IEasingFunction GetInQuad()
        {
            return new InQuadFunction();
        }

        public IEasingFunction GetOutQuad()
        {
            return new OutQuadFunction();
        }

        public IEasingFunction GetInOutQuad()
        {
            return new InOutQuadFunction();
        }

        public IEasingFunction GetLinear()
        {
            return new LinearFunction();
        }

        public IEasingFunction GetInSine()
        {
            return new InSineFunction();
        }

        public IEasingFunction GetOutSine()
        {
            return new OutSineFunction();
        }

        public IEasingFunction GetInOutSine()
        {
            return new InOutSineFunction();
        }

        public IEasingFunction GetInCubic()
        {
            return new InCubicFunction();
        }

        public IEasingFunction GetOutCubic()
        {
            return new OutCubicFunction();
        }

        public IEasingFunction GetInOutCubic()
        {
            return new InOutCubicFunction();
        }

        public IEasingFunction GetInCirc()
        {
            return new InCircFunction();
        }

        public IEasingFunction GetOutCirc()
        {
            return new OutCircFunction();
        }

        public IEasingFunction GetInOutCirc()
        {
            return new InOutCircFunction();
        }

        public IEasingFunction GetInElastic()
        {
            return new InElasticFunction();
        }

        public IEasingFunction GetOutElastic()
        {
            return new OutElasticFunction();
        }

        public IEasingFunction GetInOutElastic()
        {
            return new InOutElasticFunction();
        }

        public IEasingFunction GetInBack()
        {
            return new InBackFunction();
        }

        public IEasingFunction GetOutBack()
        {
            return new OutBackFunction();
        }

        public IEasingFunction GetInOutBack()
        {
            return new InOutBackFunction();
        }

        public IEasingFunction GetInBounce()
        {
            return new InBounceFunction();
        }

        public IEasingFunction GetOutBounce()
        {
            return new OutBounceFunction();
        }

        public IEasingFunction GetInOutBounce()
        {
            return new InOutBounceFunction();
        }
    }
}