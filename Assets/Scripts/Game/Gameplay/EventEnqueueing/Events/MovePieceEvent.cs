using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class MovePieceEvent : IEvent
    {
        public readonly IPiece Piece;
        public readonly int RowOffset;
        public readonly int ColumnOffset;
        public readonly MovePieceReason MovePieceReason;

        public MovePieceEvent(IPiece piece, int rowOffset, int columnOffset, MovePieceReason movePieceReason)
        {
            Piece = piece;
            RowOffset = rowOffset;
            ColumnOffset = columnOffset;
            MovePieceReason = movePieceReason;
        }
    }
}