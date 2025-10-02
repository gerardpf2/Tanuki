using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class MovePieceEvent : IEvent
    {
        public readonly uint Id;
        public readonly int RowOffset;
        public readonly int ColumnOffset;
        public readonly MovePieceReason MovePieceReason;

        public MovePieceEvent(uint id, int rowOffset, int columnOffset, MovePieceReason movePieceReason)
        {
            Id = id;
            RowOffset = rowOffset;
            ColumnOffset = columnOffset;
            MovePieceReason = movePieceReason;
        }
    }
}