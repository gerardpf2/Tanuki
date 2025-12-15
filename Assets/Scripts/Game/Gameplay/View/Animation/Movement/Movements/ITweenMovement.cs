using Infrastructure.Tweening.Builders;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Animation.Movement.Movements
{
    public interface ITweenMovement : IMovement
    {
        [NotNull]
        ITweenBuilder<Vector3> TweenBuilder { get; }
    }
}