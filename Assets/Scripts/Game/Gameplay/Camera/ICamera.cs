namespace Game.Gameplay.Camera
{
    public interface ICamera
    {
        int TopRow { get; }

        int BottomRow { get; }

        int VisibleRows { get; }

        bool UpdatePositionY();
    }
}