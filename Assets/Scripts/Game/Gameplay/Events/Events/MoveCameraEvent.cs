using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.Events.Events
{
    public class MoveCameraEvent : IEvent
    {
        public readonly int RowOffset;
        public readonly MoveCameraReason MoveCameraReason;

        public MoveCameraEvent(int rowOffset, MoveCameraReason moveCameraReason)
        {
            RowOffset = rowOffset;
            MoveCameraReason = moveCameraReason;
        }
    }
}