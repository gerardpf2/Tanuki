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
        IPiece GetBlockI();

        [NotNull]
        IPiece GetBlockO();

        [NotNull]
        IPiece GetBlockT();

        [NotNull]
        IPiece GetBlockJ();

        [NotNull]
        IPiece GetBlockL();

        [NotNull]
        IPiece GetBlockS();

        [NotNull]
        IPiece GetBlockZ();

        [NotNull]
        IPiece GetStaticBlock();

        [NotNull]
        IPiece GetTest();
    }
}