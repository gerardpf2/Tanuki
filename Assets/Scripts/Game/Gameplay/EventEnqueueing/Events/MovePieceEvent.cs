using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class MovePieceEvent : IEvent
    {
        public readonly IPiece Piece;
        public readonly Coordinate NewSourceCoordinate;
        public readonly MovePieceReason MovePieceReason;

        public MovePieceEvent(IPiece piece, Coordinate newSourceCoordinate, MovePieceReason movePieceReason)
        {
            Piece = piece;
            NewSourceCoordinate = newSourceCoordinate;
            MovePieceReason = movePieceReason;
        }
    }
}