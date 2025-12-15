using Game.Gameplay.Events.Reasons;

namespace Game.Gameplay.Events.Events
{
    public class MovePieceEvent : IEvent
    {
        public readonly int PieceId;
        public readonly int RowOffset;
        public readonly int ColumnOffset;
        public readonly MovePieceReason MovePieceReason;

        public MovePieceEvent(int pieceId, int rowOffset, int columnOffset, MovePieceReason movePieceReason)
        {
            PieceId = pieceId;
            RowOffset = rowOffset;
            ColumnOffset = columnOffset;
            MovePieceReason = movePieceReason;
        }
    }
}