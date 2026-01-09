using Infrastructure.Tweening.Builders;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay.View.Animation.Movement.Movements
{
    public interface ITweenMovement : IMovement
    {
        [NotNull]
        ITweenBuilder<Transform, Vector3> TweenBuilder { get; }
    }
}