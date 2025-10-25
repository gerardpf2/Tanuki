using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class LockPlayerPieceEvent : IEvent
    {
        public readonly IPiece Piece;
        public readonly Coordinate SourceCoordinate;
        public readonly Coordinate LockSourceCoordinate;
        public readonly int MovesAmount;

        public LockPlayerPieceEvent(
            IPiece piece,
            Coordinate sourceCoordinate,
            Coordinate lockSourceCoordinate,
            int movesAmount)
        {
            Piece = piece;
            SourceCoordinate = sourceCoordinate;
            LockSourceCoordinate = lockSourceCoordinate;
            MovesAmount = movesAmount;
        }
    }
}