using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class InstantiatePlayerPieceGhostEvent : IEvent
    {
        [NotNull] public readonly IPiece Piece;
        public readonly InstantiatePieceReason InstantiatePieceReason;

        public InstantiatePlayerPieceGhostEvent([NotNull] IPiece piece, InstantiatePieceReason instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(piece);

            Piece = piece;
            InstantiatePieceReason = instantiatePieceReason;
        }
    }
}