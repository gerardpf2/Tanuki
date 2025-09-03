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
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate,
            InstantiatePieceReason instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return new InstantiatePieceEvent(piece, sourceCoordinate, instantiatePieceReason);
        }

        public IEvent GetInstantiatePlayerPieceEvent([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return new InstantiatePlayerPieceEvent(piece);
        }

        public IEvent GetLockPlayerPieceEvent([NotNull] IPiece piece, Coordinate lockSourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return new LockPlayerPieceEvent(piece, lockSourceCoordinate);
        }
    }
}