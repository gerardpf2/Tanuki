using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Infrastructure.Tweening
{
    public class EasingFunctionGetter : IEasingFunctionGetter
    {
        [NotNull] private static readonly IDictionary<EasingMode, Func<float, float>> EasingFunctions =
            new Dictionary<EasingMode, Func<float, float>>
            {
                { EasingMode.EaseIn, Easing.InQuad },
                { EasingMode.EaseOut, Easing.OutQuad },
                { EasingMode.EaseInOut, Easing.InOutQuad },
                { EasingMode.Linear, Easing.Linear },
                { EasingMode.EaseInSine, Easing.InSine },
                { EasingMode.EaseOutSine, Easing.OutSine },
                { EasingMode.EaseInOutSine, Easing.InOutSine },
                { EasingMode.EaseInCubic, Easing.InCubic },
                { EasingMode.EaseOutCubic, Easing.OutCubic },
                { EasingMode.EaseInOutCubic, Easing.InOutCubic },
                { EasingMode.EaseInCirc, Easing.InCirc },
                { EasingMode.EaseOutCirc, Easing.OutCirc },
                { EasingMode.EaseInOutCirc, Easing.InOutCirc },
                { EasingMode.EaseInElastic, Easing.InElastic },
                { EasingMode.EaseOutElastic, Easing.OutElastic },
                { EasingMode.EaseInOutElastic, Easing.InOutElastic },
                { EasingMode.EaseInBack, Easing.InBack },
                { EasingMode.EaseOutBack, Easing.OutBack },
                { EasingMode.EaseInOutBack, Easing.InOutBack },
                { EasingMode.EaseInBounce, Easing.InBounce },
                { EasingMode.EaseOutBounce, Easing.OutBounce },
                { EasingMode.EaseInOutBounce, Easing.InOutBounce }
            };

        [NotNull] private static readonly IDictionary<EasingMode, EasingMode> ComplementaryEasingModes =
            new Dictionary<EasingMode, EasingMode>
            {
                { EasingMode.EaseIn, EasingMode.EaseOut },
                { EasingMode.EaseOut, EasingMode.EaseIn },
                { EasingMode.EaseInOut, EasingMode.EaseInOut },
                { EasingMode.Linear, EasingMode.Linear },
                { EasingMode.EaseInSine, EasingMode.EaseOutSine },
                { EasingMode.EaseOutSine, EasingMode.EaseInSine },
                { EasingMode.EaseInOutSine, EasingMode.EaseInOutSine },
                { EasingMode.EaseInCubic, EasingMode.EaseOutCubic },
                { EasingMode.EaseOutCubic, EasingMode.EaseInCubic },
                { EasingMode.EaseInOutCubic, EasingMode.EaseInOutCubic },
                { EasingMode.EaseInCirc, EasingMode.EaseOutCirc },
                { EasingMode.EaseOutCirc, EasingMode.EaseInCirc },
                { EasingMode.EaseInOutCirc, EasingMode.EaseInOutCirc },
                { EasingMode.EaseInElastic, EasingMode.EaseOutElastic },
                { EasingMode.EaseOutElastic, EasingMode.EaseInElastic },
                { EasingMode.EaseInOutElastic, EasingMode.EaseInOutElastic },
                { EasingMode.EaseInBack, EasingMode.EaseOutBack },
                { EasingMode.EaseOutBack, EasingMode.EaseInBack },
                { EasingMode.EaseInOutBack, EasingMode.EaseInOutBack },
                { EasingMode.EaseInBounce, EasingMode.EaseOutBounce },
                { EasingMode.EaseOutBounce, EasingMode.EaseInBounce },
                { EasingMode.EaseInOutBounce, EasingMode.EaseInOutBounce }
            };

        public Func<float, float> Get(EasingMode easingMode)
        {
            if (!EasingFunctions.TryGetValue(easingMode, out Func<float, float> easingFunction))
            {
                InvalidOperationException.Throw($"Cannot get easing function with EasingMode: {easingMode}");
            }

            return easingFunction;
        }

        public Func<float, float> GetComplementary(EasingMode easingMode)
        {
            if (!ComplementaryEasingModes.TryGetValue(easingMode, out EasingMode complementaryEasingMode))
            {
                InvalidOperationException.Throw($"Cannot get complementary easing mode with EasingMode: {easingMode}");
            }

            return Get(complementaryEasingMode);
        }
    }
}