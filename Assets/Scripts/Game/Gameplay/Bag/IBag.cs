using System.Collections.Generic;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
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