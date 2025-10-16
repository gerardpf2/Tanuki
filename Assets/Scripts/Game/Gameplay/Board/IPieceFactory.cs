using Game.Gameplay.Board.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public interface IPieceFactory
    {
        [NotNull]
        IPiece GetPlayerI();

        [NotNull]
        IPiece GetPlayerO();

        [NotNull]
        IPiece GetPlayerJ();

        [NotNull]
        IPiece GetPlayerL();

        [NotNull]
        IPiece GetTest();
    }
}