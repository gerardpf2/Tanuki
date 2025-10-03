using JetBrains.Annotations;

namespace Game.Gameplay.Camera
{
    public interface ICamera
    {
        int TopRow { get; }

        int BottomRow { get; }

        bool Update(int highestNonEmptyRow);

        [NotNull]
        ICamera Clone();
    }
}