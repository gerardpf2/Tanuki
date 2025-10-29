using Game.Gameplay.Board;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events
{
    public class EventFactory : IEventFactory
    {
        public IEvent GetInstantiatePieceEvent(
            IPiece piece,
            Coordinate sourceCoordinate,
            InstantiatePieceReason instantiatePieceReason)
        {
            IPiece pieceClone = piece.Clone(); // Clone needed so model and view boards have different piece refs

            return new InstantiatePieceEvent(pieceClone, sourceCoordinate, instantiatePieceReason);
        }

        public IEvent GetInstantiatePlayerPieceEvent(IPiece piece)
        {
            return new InstantiatePlayerPieceEvent(piece);
        }

        public IEvent GetLockPlayerPieceEvent(
            IPiece piece,
            Coordinate sourceCoordinate,
            Coordinate lockSourceCoordinate,
            int movesAmount)
        {
            IPiece pieceClone = piece.Clone(); // Clone needed so model and view boards have different piece refs

            return new LockPlayerPieceEvent(pieceClone, sourceCoordinate, lockSourceCoordinate, movesAmount);
        }

        public IEvent GetDamagePieceEvent([NotNull] IPiece piece, DamagePieceReason damagePieceReason)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return new DamagePieceEvent(piece.Id, piece.State, damagePieceReason);
        }

        public IEvent GetDestroyPieceEvent(
            int pieceId,
            DestroyPieceReason destroyPieceReason,
            DestroyPieceEvent.GoalCurrentAmountUpdatedData goalData)
        {
            return new DestroyPieceEvent(pieceId, destroyPieceReason, goalData);
        }

        public IEvent GetMovePieceEvent(int pieceId, int rowOffset, int columnOffset, MovePieceReason movePieceReason)
        {
            return new MovePieceEvent(pieceId, rowOffset, columnOffset, movePieceReason);
        }

        public MovePiecesByGravityEvent GetMovePiecesByGravityEvent()
        {
            return new MovePiecesByGravityEvent();
        }

        public IEvent GetMoveCameraEvent(int rowOffset)
        {
            return new MoveCameraEvent(rowOffset);
        }
    }
}