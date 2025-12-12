using Infrastructure.Tweening.Builders;
using Infrastructure.Unity;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Tweening.BuilderHelpers
{
    public interface ITransformTweenBuilderHelper
    {
        [NotNull]
        ITweenBuilder<Vector3> Move(Transform transform, Vector3 end, float durationS, Axis axis = Axis.All);

        [NotNull]
        ISequenceAsyncBuilder Jump(Transform transform, Vector3 end, float height, float durationS);

        [NotNull]
        ITweenBuilder<Vector3> Rotate(
            Transform transform,
            Vector3 end,
            float durationS,
            RotationType rotationType = RotationType.Full,
            Axis axis = Axis.All
        );

        [NotNull]
        ITweenBuilder<Vector3> Scale(Transform transform, Vector3 end, float durationS, Axis axis = Axis.All);
    }
}