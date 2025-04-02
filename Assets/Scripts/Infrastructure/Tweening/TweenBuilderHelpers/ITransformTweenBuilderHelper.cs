using Infrastructure.Tweening.TweenBuilders;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Tweening.TweenBuilderHelpers
{
    public interface ITransformTweenBuilderHelper
    {
        [NotNull]
        ITweenBuilder<Vector3> Move(Transform transform, Vector3 end, float durationS, Axis axis = Axis.All);

        [NotNull]
        ISequenceAsyncBuilder Jump(Transform transform, Vector3 end, float height, float durationS);

        [NotNull]
        ITweenBuilder<Vector3> Scale(Transform transform, Vector3 end, float durationS, Axis axis = Axis.All);
    }
}