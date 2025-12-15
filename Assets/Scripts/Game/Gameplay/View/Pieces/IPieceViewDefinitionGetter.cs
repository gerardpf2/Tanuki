using Game.Common.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Pieces
{
    public interface IPieceViewDefinitionGetter
    {
        [NotNull]
        IPieceViewDefinition GetBoardPiece(PieceType pieceType);

        [NotNull]
        IPieceViewDefinition GetPlayerPiece(PieceType pieceType);

        [NotNull]
        IPieceViewDefinition GetPlayerPieceGhost(PieceType pieceType);
    }
}