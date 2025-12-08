using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using JetBrains.Annotations;

namespace Game.Gameplay.Camera
{
    public interface IMoveCameraHelper
    {
        [NotNull]
        MoveCameraEvent TargetHighestPlayerPieceLockRow(
            int pieceLockSourceCoordinateRow,
            MoveCameraReason moveCameraReason
        );
    }
}