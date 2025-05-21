using System.Collections.Generic;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Parsing
{
    public interface IBoardSerializedDataConverter
    {
        void To(
            BoardSerializedData boardSerializedData,
            [NotNull] out IBoard board,
            [NotNull] out IEnumerable<IPiecePlacement> piecePlacements
        );

        [NotNull]
        BoardSerializedData From(IReadonlyBoard board);
    }
}