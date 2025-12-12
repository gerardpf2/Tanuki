using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Tweening.Builders
{
    // TODO: Remove if not needed
    public class TweenBuilderFloat : TweenBuilder<float>
    {
        public TweenBuilderFloat([NotNull] Action<float> setter, [NotNull] IEasingFunctionGetter easingFunctionGetter) : base(setter, easingFunctionGetter, Mathf.Lerp) { }
    }
}