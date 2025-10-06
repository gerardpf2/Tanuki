namespace Game.Gameplay.EventEnqueueing.Events
{
    public class SetCameraPositionEvent : IEvent
    {
        public readonly int TopRow;

        public SetCameraPositionEvent(int topRow)
        {
            TopRow = topRow;
        }
    }
}