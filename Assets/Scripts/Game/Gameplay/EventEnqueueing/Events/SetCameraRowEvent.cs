namespace Game.Gameplay.EventEnqueueing.Events
{
    public class SetCameraRowEvent : IEvent
    {
        public readonly int Row;

        public SetCameraRowEvent(int row)
        {
            Row = row;
        }
    }
}