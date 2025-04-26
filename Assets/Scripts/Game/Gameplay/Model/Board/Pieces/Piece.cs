using System.Collections.Generic;

namespace Game.Gameplay.Model.Board.Pieces
{
    public abstract class Piece : IPiece
    {
        public abstract IEnumerable<Coordinate> GetCoordinates(Coordinate sourceCoordinate);
    }
}