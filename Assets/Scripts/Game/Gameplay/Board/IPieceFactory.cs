using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public interface IPieceFactory
    {
        [NotNull]
        IPiece GetTest();

        [NotNull]
        IPiece GetPlayerBlock11();

        [NotNull]
        IPiece GetPlayerBlock12();

        [NotNull]
        IPiece GetPlayerBlock21();
    }
}