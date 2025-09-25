using System.Collections.Generic;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Parsing
{
    public interface IBoardSerializedDataConverter
    {
        void To(
            BoardSerializedData boardSerializedData,
            [NotNull] out IBoard board,
            [NotNull] out IEnumerable<PiecePlacement> piecePlacements
        );

        [NotNull]
        BoardSerializedData From(IBoard board);
    }
}