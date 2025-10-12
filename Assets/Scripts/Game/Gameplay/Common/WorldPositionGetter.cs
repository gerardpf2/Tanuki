namespace Game.Gameplay.Common
{
    public class WorldPositionGetter : IWorldPositionGetter
    {
        private const float OffsetX = 100.0f; // TODO: Set to 0
        private const float OffsetY = 1000.0f; // TODO: Set to 0
        private const float Z = 0.0f;

        public float GetX(int column)
        {
            return column + OffsetX;
        }

        public float GetY(int row)
        {
            return row + OffsetY;
        }

        public float GetZ()
        {
            return Z;
        }
    }
}