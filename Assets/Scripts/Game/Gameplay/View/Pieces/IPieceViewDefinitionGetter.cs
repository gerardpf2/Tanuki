using Game.Gameplay.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Pieces
{
    public interface IPieceViewDefinitionGetter
    {
        [NotNull]
        IPieceViewDefinition Get(PieceType pieceType);
    }
}