using System.Collections.Generic;
using Game.Common.Pieces;
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

        void Build(IEnumerable<BagPieceEntry> bagPieceEntries, IEnumerable<PieceType> initialPieceTypes);

        void ConsumeCurrent();

        void Clear();
    }
}