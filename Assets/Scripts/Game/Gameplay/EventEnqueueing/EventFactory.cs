using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.EventEnqueueing
{
    public class EventFactory : IEventFactory
    {
        public IEvent GetInstantiatePieceEvent(
            IPiece piece,
            Coordinate sourceCoordinate,
            InstantiatePieceReason instantiatePieceReason)
        {
            return new InstantiatePieceEvent(piece, sourceCoordinate, instantiatePieceReason);
        }

        public IEvent GetInstantiatePlayerPieceEvent(IPiece piece)
        {
            return new InstantiatePlayerPieceEvent(piece);
        }

        public IEvent GetLockPlayerPieceEvent(IPiece piece, Coordinate lockSourceCoordinate)
        {
            return new LockPlayerPieceEvent(piece, lockSourceCoordinate);
        }

        public IEvent GetDamagePieceEvent(IPiece piece)
        {
            return new DamagePieceEvent(piece);
        }

        public IEvent GetDestroyPieceEvent(IPiece piece, DestroyPieceReason destroyPieceReason)
        {
            return new DestroyPieceEvent(piece, destroyPieceReason);
        }

        public IEvent GetMovePieceEvent(IPiece piece, Coordinate newSourceCoordinate, MovePieceReason movePieceReason)
        {
            return new MovePieceEvent(piece, newSourceCoordinate, movePieceReason);
        }
    }
}