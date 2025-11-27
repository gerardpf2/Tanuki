using UnityEngine;

namespace Game.Gameplay.Board.Utils
{
    public static class CoordinateUtils
    {
        public static Coordinate Up(this Coordinate coordinate, int times = 1)
        {
            return coordinate.WithOffset(times, 0);
        }

        public static Coordinate Down(this Coordinate coordinate, int times = 1)
        {
            return coordinate.WithOffset(-times, 0);
        }

        public static Coordinate Right(this Coordinate coordinate, int times = 1)
        {
            return coordinate.WithOffset(0, times);
        }

        public static Coordinate Left(this Coordinate coordinate, int times = 1)
        {
            return coordinate.WithOffset(0, -times);
        }

        public static Coordinate WithOffset(this Coordinate coordinate, int rowOffset, int columnOffset)
        {
            return new Coordinate(coordinate.Row + rowOffset, coordinate.Column + columnOffset);
        }

        public static Vector3 ToVector3(this Coordinate coordinate)
        {
            return new Vector3(coordinate.Column, coordinate.Row);
        }

        public static Coordinate ToCoordinate(this Vector3 vector3)
        {
            return new Coordinate(Mathf.FloorToInt(vector3.y), Mathf.FloorToInt(vector3.x));
        }
    }
}