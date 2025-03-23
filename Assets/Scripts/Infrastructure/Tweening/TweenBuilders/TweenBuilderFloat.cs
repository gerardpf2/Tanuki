using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Tweening.TweenBuilders
{
    public class TweenBuilderFloat : TweenBuilder<float>
    {
        public TweenBuilderFloat([NotNull] IEasingFunctionGetter easingFunctionGetter) : base(easingFunctionGetter, Mathf.Lerp) { }
    }
}