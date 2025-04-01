using Infrastructure.Tweening.TweenBuilders;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Tweening.TweenBuilderHelpers
{
    public interface ITransformTweenBuilderHelper
    {
        #region Movement

        [NotNull]
        ITweenBuilder<Vector3> Move(Transform transform, Vector3 end, float durationS);

        [NotNull]
        ITweenBuilder<float> MoveX(Transform transform, float end, float durationS);

        [NotNull]
        ITweenBuilder<float> MoveY(Transform transform, float end, float durationS);

        [NotNull]
        ITweenBuilder<float> MoveZ(Transform transform, float end, float durationS);

        [NotNull]
        ITweenBuilder<Vector3> LocalMove(Transform transform, Vector3 end, float durationS);

        [NotNull]
        ITweenBuilder<float> LocalMoveX(Transform transform, float end, float durationS);

        [NotNull]
        ITweenBuilder<float> LocalMoveY(Transform transform, float end, float durationS);

        [NotNull]
        ITweenBuilder<float> LocalMoveZ(Transform transform, float end, float durationS);

        #endregion
    }
}