using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Tweening.Builders
{
    public class TweenBuilderVector3 : TweenBuilder<Vector3>
    {
        public TweenBuilderVector3(
            [NotNull] Action<Vector3> setter,
            [NotNull] IEasingFunctionGetter easingFunctionGetter) : base(setter, easingFunctionGetter, Vector3.Lerp) { }
    }
}