using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public interface IPieceFactory
    {
        [NotNull]
        IPiece GetTest(IEnumerable<KeyValuePair<string, string>> customData);

        [NotNull]
        IPiece GetPlayerBlock11();

        [NotNull]
        IPiece GetPlayerBlock12();

        [NotNull]
        IPiece GetPlayerBlock21();
    }
}