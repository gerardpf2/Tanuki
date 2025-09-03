using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public interface IPieceGetter
    {
        [NotNull]
        IPiece Get(PieceType pieceType, IEnumerable<KeyValuePair<string, string>> customData);
    }
}