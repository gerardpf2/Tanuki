using Game.Gameplay.Pieces.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces
{
    public interface IPieceFactory
    {
        [NotNull]
        IPiece GetI();

        [NotNull]
        IPiece GetO();

        [NotNull]
        IPiece GetT();

        [NotNull]
        IPiece GetJ();

        [NotNull]
        IPiece GetL();

        [NotNull]
        IPiece GetS();

        [NotNull]
        IPiece GetZ();

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
        IPiece GetTato();

        [NotNull]
        IPiece GetTata();

        [NotNull]
        IPiece GetTete();

        [NotNull]
        IPiece GetTest();
    }
}