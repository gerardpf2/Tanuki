using System.Collections.Generic;
using JetBrains.Annotations;

namespace Game.Gameplay.Model.Board.Pieces
{
    public interface IPiece
    {
        [NotNull]
        IEnumerable<Coordinate> GetCoordinates(Coordinate sourceCoordinate);
    }
}