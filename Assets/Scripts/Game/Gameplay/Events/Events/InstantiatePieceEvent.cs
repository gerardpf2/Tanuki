using Game.Gameplay.Board;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
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

            Piece = piece.Clone(); // Clone needed so model and view boards have different piece refs
            SourceCoordinate = sourceCoordinate;
            InstantiatePieceReason = instantiatePieceReason;
        }
    }
}