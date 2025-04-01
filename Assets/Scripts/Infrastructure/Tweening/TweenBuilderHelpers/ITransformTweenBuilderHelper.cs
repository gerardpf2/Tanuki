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
        ITweenBuilder<Vector3> MoveX(Transform transform, float end, float durationS);

        [NotNull]
        ITweenBuilder<Vector3> MoveY(Transform transform, float end, float durationS);

        [NotNull]
        ITweenBuilder<Vector3> MoveZ(Transform transform, float end, float durationS);

        [NotNull]
        ITweenBuilder<Vector3> LocalMove(Transform transform, Vector3 end, float durationS);

        [NotNull]
        ITweenBuilder<Vector3> LocalMoveX(Transform transform, float end, float durationS);

        [NotNull]
        ITweenBuilder<Vector3> LocalMoveY(Transform transform, float end, float durationS);

        [NotNull]
        ITweenBuilder<Vector3> LocalMoveZ(Transform transform, float end, float durationS);

        #endregion
    }
}