namespace Game.Gameplay.Board.Utils
{
    public static class CoordinateUtils
    {
        public static Coordinate WithOffset(this Coordinate coordinate, int rowOffset, int columnOffset)
        {
            return new Coordinate(coordinate.Row + rowOffset, coordinate.Column + columnOffset);
        }
    }
}