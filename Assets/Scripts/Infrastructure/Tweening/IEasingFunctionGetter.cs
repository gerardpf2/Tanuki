using System;
using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace Infrastructure.Tweening
{
    public interface IEasingFunctionGetter
    {
        [NotNull]
        Func<float, float> Get(EasingMode easingMode);

        [NotNull]
        Func<float, float> GetComplementary(EasingMode easingMode);
    }
}