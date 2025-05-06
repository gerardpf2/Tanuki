namespace Game.Gameplay.View.Camera
{
    public interface ICameraBoardViewGetter
    {
        int VisibleTopRow { get; }

        int Column { get; }
    }
}