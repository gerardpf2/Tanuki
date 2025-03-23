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
        [NotNull] private readonly IDictionary<EasingMode, Func<float, float>> _easingFunctions =
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

        public Func<float, float> Get(EasingMode easingMode)
        {
            if (!_easingFunctions.TryGetValue(easingMode, out Func<float, float> easingFunction))
            {
                InvalidOperationException.Throw($"Cannot get easing function with EasingMode: {easingMode}");
            }

            return easingFunction;
        }
    }
}