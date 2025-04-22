using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Tweening.Builders
{
    public class TweenBuilderFloat : TweenBuilder<float>
    {
        public TweenBuilderFloat([NotNull] IEasingFunctionGetter easingFunctionGetter) : base(easingFunctionGetter, Mathf.Lerp) { }
    }
}