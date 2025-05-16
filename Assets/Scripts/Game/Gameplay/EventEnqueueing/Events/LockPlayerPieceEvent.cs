using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class LockPlayerPieceEvent : IEvent
    {
        [NotNull] public readonly IPiece Piece;
        public readonly Coordinate LockSourceCoordinate;

        public LockPlayerPieceEvent([NotNull] IPiece piece, Coordinate lockSourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            Piece = piece;
            LockSourceCoordinate = lockSourceCoordinate;
        }
    }
}