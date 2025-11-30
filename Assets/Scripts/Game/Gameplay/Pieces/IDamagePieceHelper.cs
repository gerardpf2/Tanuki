using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces
{
    public interface IDamagePieceHelper
    {
        [NotNull]
        DamagePieceEvent Damage(
            int pieceId,
            Coordinate coordinate,
            DamagePieceReason damagePieceReason,
            Direction direction
        );
    }
}