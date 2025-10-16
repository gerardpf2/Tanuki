using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public interface IPieceFactory
    {
        [NotNull]
        IPiece GetTest();

        [NotNull]
        IPiece GetPlayerO1();

        [NotNull]
        IPiece GetPlayerI1();
    }
}