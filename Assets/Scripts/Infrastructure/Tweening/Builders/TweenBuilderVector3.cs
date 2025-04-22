using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Tweening.Builders
{
    public class TweenBuilderVector3 : TweenBuilder<Vector3>
    {
        public TweenBuilderVector3([NotNull] IEasingFunctionGetter easingFunctionGetter) : base(easingFunctionGetter, Vector3.Lerp) { }
    }
}