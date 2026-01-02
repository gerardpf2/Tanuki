using Game.Gameplay.Board;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events.Events
{
    public class LockPlayerPieceEvent : IEvent
    {
        [NotNull] public readonly MovePlayerPieceEvent MovePlayerPieceEvent;
        [NotNull] public readonly DestroyPlayerPieceEvent DestroyPlayerPieceEvent;
        [NotNull] public readonly InstantiatePieceEvent InstantiatePieceEvent;
        [NotNull] public readonly SetMovesAmountEvent SetMovesAmountEvent;

        public LockPlayerPieceEvent(
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate,
            Coordinate lockSourceCoordinate,
            int movesAmount)
        {
            ArgumentNullException.ThrowIfNull(piece);

            int rowOffset = lockSourceCoordinate.Row - sourceCoordinate.Row;
            int columnOffset = lockSourceCoordinate.Column - sourceCoordinate.Column;

            MovePlayerPieceEvent = new MovePlayerPieceEvent(rowOffset, columnOffset, MovePieceReason.Lock);
            DestroyPlayerPieceEvent = new DestroyPlayerPieceEvent(DestroyPieceReason.Lock);
            InstantiatePieceEvent = new InstantiatePieceEvent(piece, lockSourceCoordinate, InstantiatePieceReason.Lock);
            SetMovesAmountEvent = new SetMovesAmountEvent(movesAmount);
        }
    }
}