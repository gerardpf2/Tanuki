namespace Game.Gameplay.EventEnqueueing.Events
{
    public class MoveCameraEvent : IEvent
    {
        public readonly int RowOffset;

        public MoveCameraEvent(int rowOffset)
        {
            RowOffset = rowOffset;
        }
    }
}