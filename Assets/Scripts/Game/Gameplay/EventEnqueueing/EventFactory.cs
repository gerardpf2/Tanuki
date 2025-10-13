using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.EventEnqueueing
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

        public IEvent GetLockPlayerPieceEvent(IPiece piece, Coordinate lockSourceCoordinate, int movesAmount)
        {
            IPiece pieceClone = piece.Clone(); // Clone needed so model and view boards have different piece refs

            return new LockPlayerPieceEvent(pieceClone, lockSourceCoordinate, movesAmount);
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

        public IEvent GetSetCameraRowEvent(int topRow)
        {
            return new SetCameraRowEvent(topRow);
        }
    }
}