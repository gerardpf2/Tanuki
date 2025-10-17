using System.Collections.Generic;
using Game.Gameplay.Board;
using JetBrains.Annotations;

namespace Game.Gameplay.Bag
{
    public interface IBag
    {
        [NotNull, ItemNotNull]
        IEnumerable<BagPieceEntry> BagPieceEntries { get; }

        [NotNull]
        IEnumerable<PieceType> InitialPieceTypes { get; }

        PieceType Current { get; }

        void ConsumeCurrent();
    }
}