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

        public IEvent GetLockPlayerPieceEvent(IPiece piece, Coordinate lockSourceCoordinate)
        {
            IPiece pieceClone = piece.Clone(); // Clone needed so model and view boards have different piece refs

            return new LockPlayerPieceEvent(pieceClone, lockSourceCoordinate);
        }

        public IEvent GetDamagePieceEvent([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return new DamagePieceEvent(piece.Id, piece.State);
        }

        public IEvent GetDestroyPieceEvent(uint id, DestroyPieceReason destroyPieceReason)
        {
            return new DestroyPieceEvent(id, destroyPieceReason);
        }

        public IEvent GetMovePieceEvent(uint id, int rowOffset, int columnOffset, MovePieceReason movePieceReason)
        {
            return new MovePieceEvent(id, rowOffset, columnOffset, movePieceReason);
        }
    }
}