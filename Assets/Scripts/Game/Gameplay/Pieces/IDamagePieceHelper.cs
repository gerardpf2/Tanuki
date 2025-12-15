using System.Collections.Generic;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces
{
    public interface IDamagePieceHelper
    {
        DamagePieceEvent Damage(Coordinate coordinate, DamagePieceReason damagePieceReason, Direction direction);

        [NotNull, ItemNotNull]
        IEnumerable<DamagePieceEvent> Damage(
            IEnumerable<Coordinate> coordinates,
            DamagePieceReason damagePieceReason,
            Direction direction
        );
    }
}