using System;
using JetBrains.Annotations;
using UnityEngine.UIElements;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Tweening
{
    public class EasingFunctionGetter : IEasingFunctionGetter
    {
        [NotNull] private readonly IEasingFunctionFactory _easingFunctionFactory;

        public EasingFunctionGetter([NotNull] IEasingFunctionFactory easingFunctionFactory)
        {
            ArgumentNullException.ThrowIfNull(easingFunctionFactory);

            _easingFunctionFactory = easingFunctionFactory;
        }

        public Func<float, float> Get(EasingMode easingMode)
        {
            switch (easingMode)
            {
                case EasingMode.EaseIn:
                    return _easingFunctionFactory.GetInQuad();
                case EasingMode.EaseOut:
                    return _easingFunctionFactory.GetOutQuad();
                case EasingMode.EaseInOut:
                    return _easingFunctionFactory.GetInOutQuad();
                case EasingMode.Linear:
                    return _easingFunctionFactory.GetLinear();
                case EasingMode.EaseInSine:
                    return _easingFunctionFactory.GetInSine();
                case EasingMode.EaseOutSine:
                    return _easingFunctionFactory.GetOutSine();
                case EasingMode.EaseInOutSine:
                    return _easingFunctionFactory.GetInOutSine();
                case EasingMode.EaseInCubic:
                    return _easingFunctionFactory.GetInCubic();
                case EasingMode.EaseOutCubic:
                    return _easingFunctionFactory.GetOutCubic();
                case EasingMode.EaseInOutCubic:
                    return _easingFunctionFactory.GetInOutCubic();
                case EasingMode.EaseInCirc:
                    return _easingFunctionFactory.GetInCirc();
                case EasingMode.EaseOutCirc:
                    return _easingFunctionFactory.GetOutCirc();
                case EasingMode.EaseInOutCirc:
                    return _easingFunctionFactory.GetInOutCirc();
                case EasingMode.EaseInElastic:
                    return _easingFunctionFactory.GetInElastic();
                case EasingMode.EaseOutElastic:
                    return _easingFunctionFactory.GetOutElastic();
                case EasingMode.EaseInOutElastic:
                    return _easingFunctionFactory.GetInOutElastic();
                case EasingMode.EaseInBack:
                    return _easingFunctionFactory.GetInBack();
                case EasingMode.EaseOutBack:
                    return _easingFunctionFactory.GetOutBack();
                case EasingMode.EaseInOutBack:
                    return _easingFunctionFactory.GetInOutBack();
                case EasingMode.EaseInBounce:
                    return _easingFunctionFactory.GetInBounce();
                case EasingMode.EaseOutBounce:
                    return _easingFunctionFactory.GetOutBounce();
                case EasingMode.EaseInOutBounce:
                    return _easingFunctionFactory.GetInOutBounce();
                case EasingMode.Ease:
                    InvalidOperationException.Throw($"Cannot get easing function for {easingMode}");
                    return null;
                default:
                    ArgumentOutOfRangeException.Throw(easingMode);
                    return null;
            }
        }

        public Func<float, float> GetComplementary(EasingMode easingMode)
        {
            return Get(GetComplementaryEasingMode(easingMode));
        }

        private static EasingMode GetComplementaryEasingMode(EasingMode easingMode)
        {
            switch (easingMode)
            {
                case EasingMode.EaseIn:
                    return EasingMode.EaseOut;
                case EasingMode.EaseOut:
                    return EasingMode.EaseIn;
                case EasingMode.EaseInOut:
                    return EasingMode.EaseInOut;
                case EasingMode.Linear:
                    return EasingMode.Linear;
                case EasingMode.EaseInSine:
                    return EasingMode.EaseOutSine;
                case EasingMode.EaseOutSine:
                    return EasingMode.EaseInSine;
                case EasingMode.EaseInOutSine:
                    return EasingMode.EaseInOutSine;
                case EasingMode.EaseInCubic:
                    return EasingMode.EaseOutCubic;
                case EasingMode.EaseOutCubic:
                    return EasingMode.EaseInCubic;
                case EasingMode.EaseInOutCubic:
                    return EasingMode.EaseInOutCubic;
                case EasingMode.EaseInCirc:
                    return EasingMode.EaseOutCirc;
                case EasingMode.EaseOutCirc:
                    return EasingMode.EaseInCirc;
                case EasingMode.EaseInOutCirc:
                    return EasingMode.EaseInOutCirc;
                case EasingMode.EaseInElastic:
                    return EasingMode.EaseOutElastic;
                case EasingMode.EaseOutElastic:
                    return EasingMode.EaseInElastic;
                case EasingMode.EaseInOutElastic:
                    return EasingMode.EaseInOutElastic;
                case EasingMode.EaseInBack:
                    return EasingMode.EaseOutBack;
                case EasingMode.EaseOutBack:
                    return EasingMode.EaseInBack;
                case EasingMode.EaseInOutBack:
                    return EasingMode.EaseInOutBack;
                case EasingMode.EaseInBounce:
                    return EasingMode.EaseOutBounce;
                case EasingMode.EaseOutBounce:
                    return EasingMode.EaseInBounce;
                case EasingMode.EaseInOutBounce:
                    return EasingMode.EaseInOutBounce;
                case EasingMode.Ease:
                    InvalidOperationException.Throw($"Cannot get complementary easing mode for {easingMode}");
                    return EasingMode.Ease;
                default:
                    ArgumentOutOfRangeException.Throw(easingMode);
                    return EasingMode.Ease;
            }
        }
    }
}