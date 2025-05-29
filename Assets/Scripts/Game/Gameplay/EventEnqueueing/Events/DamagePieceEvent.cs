using Game.Gameplay.Board.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class DamagePieceEvent : IEvent
    {
        [NotNull] public readonly IPiece Piece;

        public DamagePieceEvent([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);
            
            Piece = piece;
        }
    }
}