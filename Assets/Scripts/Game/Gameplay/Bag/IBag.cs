using System.Collections.Generic;
using Game.Gameplay.Pieces;
using Game.Gameplay.Pieces.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Bag
{
    public interface IBag
    {
        [NotNull, ItemNotNull]
        IEnumerable<BagPieceEntry> BagPieceEntries { get; }

        [NotNull]
        IEnumerable<PieceType> InitialPieceTypes { get; }

        [NotNull]
        IPiece Current { get; }

        void ConsumeCurrent();
    }
}