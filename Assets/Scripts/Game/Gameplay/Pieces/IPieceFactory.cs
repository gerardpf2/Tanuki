using Game.Gameplay.Pieces.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces
{
    public interface IPieceFactory
    {
        [NotNull]
        IPiece GetPlayerI();

        [NotNull]
        IPiece GetPlayerO();

        [NotNull]
        IPiece GetPlayerT();

        [NotNull]
        IPiece GetPlayerJ();

        [NotNull]
        IPiece GetPlayerL();

        [NotNull]
        IPiece GetPlayerS();

        [NotNull]
        IPiece GetPlayerZ();

        [NotNull]
        IPiece GetTest();
    }
}