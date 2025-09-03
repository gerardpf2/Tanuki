using System.Collections.Generic;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Parsing
{
    public interface IBoardParser
    {
        string Serialize(IReadonlyBoard board);

        void Deserialize(
            string value,
            [NotNull] out IBoard board,
            [NotNull] out IEnumerable<PiecePlacement> piecePlacements
        );
    }
}