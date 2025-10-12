namespace Game.Gameplay.Common
{
    public interface IWorldPositionGetter
    {
        float GetX(int column);

        float GetY(int row);

        float GetZ();
    }
}