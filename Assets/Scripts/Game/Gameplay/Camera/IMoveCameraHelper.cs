using Game.Gameplay.Events.Events;
using JetBrains.Annotations;

namespace Game.Gameplay.Camera
{
    public interface IMoveCameraHelper
    {
        [NotNull]
        MoveCameraEvent TargetHighestPlayerPieceLockRow(int pieceLockSourceCoordinateRow);
    }
}