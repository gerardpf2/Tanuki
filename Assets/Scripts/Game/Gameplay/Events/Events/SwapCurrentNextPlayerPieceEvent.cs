using Game.Common.Pieces;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class SwapCurrentNextPlayerPieceEvent : IEvent
    {
        [NotNull] public readonly DestroyPlayerPieceEvent DestroyPlayerPieceEvent;
        [NotNull] public readonly InstantiatePlayerPieceEvent InstantiatePlayerPieceEvent;

        public SwapCurrentNextPlayerPieceEvent(
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate,
            PieceType nextPieceType)
        {
            ArgumentNullException.ThrowIfNull(piece);

            DestroyPlayerPieceEvent = new DestroyPlayerPieceEvent(DestroyPieceReason.SwapCurrentNext);

            InstantiatePlayerPieceEvent =
                new InstantiatePlayerPieceEvent(
                    piece,
                    sourceCoordinate,
                    nextPieceType,
                    InstantiatePieceReason.SwapCurrentNext
                );
        }
    }
}