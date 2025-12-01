using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.Pieces
{
    public interface IDamagePieceHelper
    {
        DamagePieceEvent Damage(Coordinate coordinate, DamagePieceReason damagePieceReason, Direction direction);
    }
}