using Game.Gameplay.Board.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class LockPlayerPieceEvent : IEvent
    {
        [NotNull] public readonly IPiece Piece;

        public LockPlayerPieceEvent([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            Piece = piece;
        }
    }
}