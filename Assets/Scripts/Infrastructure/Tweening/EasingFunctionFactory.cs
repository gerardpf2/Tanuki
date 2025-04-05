using System;
using UnityEngine.UIElements.Experimental;

namespace Infrastructure.Tweening
{
    public class EasingFunctionFactory : IEasingFunctionFactory
    {
        public Func<float, float> GetInQuad()
        {
            return Easing.InQuad;
        }

        public Func<float, float> GetOutQuad()
        {
            return Easing.OutQuad;
        }

        public Func<float, float> GetInOutQuad()
        {
            return Easing.InOutQuad;
        }

        public Func<float, float> GetLinear()
        {
            return Easing.Linear;
        }

        public Func<float, float> GetInSine()
        {
            return Easing.InSine;
        }

        public Func<float, float> GetOutSine()
        {
            return Easing.OutSine;
        }

        public Func<float, float> GetInOutSine()
        {
            return Easing.InOutSine;
        }

        public Func<float, float> GetInCubic()
        {
            return Easing.InCubic;
        }

        public Func<float, float> GetOutCubic()
        {
            return Easing.OutCubic;
        }

        public Func<float, float> GetInOutCubic()
        {
            return Easing.InOutCubic;
        }

        public Func<float, float> GetInCirc()
        {
            return Easing.InCirc;
        }

        public Func<float, float> GetOutCirc()
        {
            return Easing.OutCirc;
        }

        public Func<float, float> GetInOutCirc()
        {
            return Easing.InOutCirc;
        }

        public Func<float, float> GetInElastic()
        {
            return Easing.InElastic;
        }

        public Func<float, float> GetOutElastic()
        {
            return Easing.OutElastic;
        }

        public Func<float, float> GetInOutElastic()
        {
            return Easing.InOutElastic;
        }

        public Func<float, float> GetInBack()
        {
            return Easing.InBack;
        }

        public Func<float, float> GetOutBack()
        {
            return Easing.OutBack;
        }

        public Func<float, float> GetInOutBack()
        {
            return Easing.InOutBack;
        }

        public Func<float, float> GetInBounce()
        {
            return Easing.InBounce;
        }

        public Func<float, float> GetOutBounce()
        {
            return Easing.OutBounce;
        }

        public Func<float, float> GetInOutBounce()
        {
            return Easing.InOutBounce;
        }
    }
}