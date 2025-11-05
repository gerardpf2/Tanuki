using Game.Common.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Pieces
{
    public interface IPieceViewDefinitionGetter
    {
        [NotNull]
        IPieceViewDefinition Get(PieceType pieceType);

        [NotNull]
        IPieceViewDefinition GetGhost(PieceType pieceType);
    }
}