namespace Game.Gameplay.Camera
{
    public interface ICamera
    {
        int TopRow { get; set; }

        int BottomRow { get; }

        int VisibleRows { get; }

        int ExtraRowsOnTop { get; }

        void Initialize();

        void Uninitialize();
    }
}