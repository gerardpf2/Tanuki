namespace Game.Common.Utils
{
    public static class DirectionUtils
    {
        private const int MaxDirections = 4;

        public static Direction GetRotated(this Direction direction, int steps)
        {
            return (Direction)(((int)direction + steps) % MaxDirections);
        }
    }
}