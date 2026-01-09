using Game.Gameplay.Board;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class InstantiatePlayerPieceEvent : IEvent
    {
        [NotNull] public readonly IPiece Piece;
        public readonly Coordinate SourceCoordinate;
        public readonly InstantiatePieceReason InstantiatePieceReason;
        [NotNull] public readonly InstantiatePlayerPieceGhostEvent InstantiatePlayerPieceGhostEvent;

        public InstantiatePlayerPieceEvent(
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate,
            InstantiatePieceReason instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(piece);

            Piece = piece;
            SourceCoordinate = sourceCoordinate;
            InstantiatePieceReason = instantiatePieceReason;
            InstantiatePlayerPieceGhostEvent = new InstantiatePlayerPieceGhostEvent(piece, instantiatePieceReason);
        }
    }
}