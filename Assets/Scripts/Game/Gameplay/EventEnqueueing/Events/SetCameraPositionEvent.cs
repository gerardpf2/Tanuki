namespace Game.Gameplay.EventEnqueueing.Events
{
    public class SetCameraPositionEvent : IEvent
    {
        public readonly int TopRow;
        public readonly int BottomRow;

        public SetCameraPositionEvent(int topRow, int bottomRow)
        {
            TopRow = topRow;
            BottomRow = bottomRow;
        }
    }
}