namespace Game.Gameplay.Camera
{
    public interface ICamera
    {
        int TopRow { get; set; }

        int BottomRow { get; set; }

        int VisibleRows { get; }

        void Initialize();

        void Uninitialize();
    }
}