using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.Events.Events
{
    public class MovePlayerPieceEvent : IEvent
    {
        public readonly int RowOffset;
        public readonly int ColumnOffset;
        public readonly MovePieceReason MovePieceReason;

        public MovePlayerPieceEvent(int rowOffset, int columnOffset, MovePieceReason movePieceReason)
        {
            RowOffset = rowOffset;
            ColumnOffset = columnOffset;
            MovePieceReason = movePieceReason;
        }
    }
}