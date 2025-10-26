using Game.Gameplay.Board;
using Game.Gameplay.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.View.Board
{
    public interface IPieceViewDefinitionGetter
    {
        [NotNull]
        IPieceViewDefinition Get(PieceType pieceType);
    }
}