using System.Collections.Generic;

namespace Game.Gameplay.Model.Board.Pieces
{
    public class Test : Piece
    {
        public override IEnumerable<Coordinate> GetCoordinates(Coordinate sourceCoordinate)
        {
            yield return sourceCoordinate;
        }
    }
}