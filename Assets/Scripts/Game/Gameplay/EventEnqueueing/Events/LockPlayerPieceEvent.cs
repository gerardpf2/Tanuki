using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class LockPlayerPieceEvent : IEvent
    {
        public readonly IPiece Piece;
        public readonly Coordinate LockSourceCoordinate;

        public LockPlayerPieceEvent(IPiece piece, Coordinate lockSourceCoordinate)
        {
            Piece = piece;
            LockSourceCoordinate = lockSourceCoordinate;
        }
    }
}