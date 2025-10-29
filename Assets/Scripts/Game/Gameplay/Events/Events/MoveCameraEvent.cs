namespace Game.Gameplay.Events.Events
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