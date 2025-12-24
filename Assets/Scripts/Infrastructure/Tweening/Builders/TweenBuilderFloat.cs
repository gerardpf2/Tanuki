using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Tweening.Builders
{
    // TODO: Remove if not needed
    public class TweenBuilderFloat<TTarget> : TweenBuilder<TTarget, float>
    {
        public TweenBuilderFloat(
            [NotNull] Action<TTarget, float> setter,
            [NotNull] IEasingFunctionGetter easingFunctionGetter) : base(setter, easingFunctionGetter, Mathf.Lerp) { }
    }
}