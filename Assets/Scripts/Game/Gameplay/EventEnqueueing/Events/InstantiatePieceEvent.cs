using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.EventEnqueueing.Events
{
    public class InstantiatePieceEvent : IEvent
    {
        [NotNull] public readonly IPiece Piece;
        public readonly Coordinate SourceCoordinate;
        public readonly InstantiatePieceReason InstantiatePieceReason;

        public InstantiatePieceEvent(
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate,
            InstantiatePieceReason instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(piece);

            Piece = piece;
            SourceCoordinate = sourceCoordinate;
            InstantiatePieceReason = instantiatePieceReason;
        }
    }
}