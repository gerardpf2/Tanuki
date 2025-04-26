using Game.Gameplay.Model.Board.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Model.Board
{
    public interface IPieceFactory
    {
        [NotNull]
        IPiece GetTest();
    }
}