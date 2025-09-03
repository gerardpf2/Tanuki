using System.Collections.Generic;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public interface IPiece
    {
        bool Alive { get; }

        [NotNull]
        IEnumerable<Coordinate> GetCoordinates(Coordinate sourceCoordinate);
    }
}