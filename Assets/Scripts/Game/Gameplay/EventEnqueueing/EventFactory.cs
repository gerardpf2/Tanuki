using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.EventEnqueueing.Events.Reasons;

namespace Game.Gameplay.EventEnqueueing
{
    public class EventFactory : IEventFactory
    {
        public IEvent GetInstantiate(
            IPiece piece,
            PieceType pieceType,
            Coordinate sourceCoordinate,
            InstantiateReason instantiateReason)
        {
            return new InstantiateEvent(piece, pieceType, sourceCoordinate, instantiateReason);
        }

        public IEvent GetInstantiatePlayerPiece(IPiece piece, PieceType pieceType)
        {
            return new InstantiatePlayerPieceEvent(piece, pieceType);
        }
    }
}