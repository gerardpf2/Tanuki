using Infrastructure.Tweening.EasingFunctions;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;

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

        public IEasingFunction Get(EasingType easingType)
        {
            switch (easingType)
            {
                case EasingType.InQuad:
                    return _easingFunctionFactory.GetInQuad();
                case EasingType.OutQuad:
                    return _easingFunctionFactory.GetOutQuad();
                case EasingType.InOutQuad:
                    return _easingFunctionFactory.GetInOutQuad();
                case EasingType.Linear:
                    return _easingFunctionFactory.GetLinear();
                case EasingType.InSine:
                    return _easingFunctionFactory.GetInSine();
                case EasingType.OutSine:
                    return _easingFunctionFactory.GetOutSine();
                case EasingType.InOutSine:
                    return _easingFunctionFactory.GetInOutSine();
                case EasingType.InCubic:
                    return _easingFunctionFactory.GetInCubic();
                case EasingType.OutCubic:
                    return _easingFunctionFactory.GetOutCubic();
                case EasingType.InOutCubic:
                    return _easingFunctionFactory.GetInOutCubic();
                case EasingType.InCirc:
                    return _easingFunctionFactory.GetInCirc();
                case EasingType.OutCirc:
                    return _easingFunctionFactory.GetOutCirc();
                case EasingType.InOutCirc:
                    return _easingFunctionFactory.GetInOutCirc();
                case EasingType.InElastic:
                    return _easingFunctionFactory.GetInElastic();
                case EasingType.OutElastic:
                    return _easingFunctionFactory.GetOutElastic();
                case EasingType.InOutElastic:
                    return _easingFunctionFactory.GetInOutElastic();
                case EasingType.InBack:
                    return _easingFunctionFactory.GetInBack();
                case EasingType.OutBack:
                    return _easingFunctionFactory.GetOutBack();
                case EasingType.InOutBack:
                    return _easingFunctionFactory.GetInOutBack();
                case EasingType.InBounce:
                    return _easingFunctionFactory.GetInBounce();
                case EasingType.OutBounce:
                    return _easingFunctionFactory.GetOutBounce();
                case EasingType.InOutBounce:
                    return _easingFunctionFactory.GetInOutBounce();
                default:
                    ArgumentOutOfRangeException.Throw(easingType);
                    return null;
            }
        }

        public IEasingFunction GetComplementary(EasingType easingType)
        {
            return Get(GetComplementaryEasingType(easingType));
        }

        private static EasingType GetComplementaryEasingType(EasingType easingType)
        {
            switch (easingType)
            {
                case EasingType.InQuad:
                    return EasingType.OutQuad;
                case EasingType.OutQuad:
                    return EasingType.InQuad;
                case EasingType.InOutQuad:
                    return EasingType.InOutQuad;
                case EasingType.Linear:
                    return EasingType.Linear;
                case EasingType.InSine:
                    return EasingType.OutSine;
                case EasingType.OutSine:
                    return EasingType.InSine;
                case EasingType.InOutSine:
                    return EasingType.InOutSine;
                case EasingType.InCubic:
                    return EasingType.OutCubic;
                case EasingType.OutCubic:
                    return EasingType.InCubic;
                case EasingType.InOutCubic:
                    return EasingType.InOutCubic;
                case EasingType.InCirc:
                    return EasingType.OutCirc;
                case EasingType.OutCirc:
                    return EasingType.InCirc;
                case EasingType.InOutCirc:
                    return EasingType.InOutCirc;
                case EasingType.InElastic:
                    return EasingType.OutElastic;
                case EasingType.OutElastic:
                    return EasingType.InElastic;
                case EasingType.InOutElastic:
                    return EasingType.InOutElastic;
                case EasingType.InBack:
                    return EasingType.OutBack;
                case EasingType.OutBack:
                    return EasingType.InBack;
                case EasingType.InOutBack:
                    return EasingType.InOutBack;
                case EasingType.InBounce:
                    return EasingType.OutBounce;
                case EasingType.OutBounce:
                    return EasingType.InBounce;
                case EasingType.InOutBounce:
                    return EasingType.InOutBounce;
                default:
                    ArgumentOutOfRangeException.Throw(easingType);
                    return easingType;
            }
        }
    }
}