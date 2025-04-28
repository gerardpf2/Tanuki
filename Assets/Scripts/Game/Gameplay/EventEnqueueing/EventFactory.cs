using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events;

namespace Game.Gameplay.EventEnqueueing
{
    public class EventFactory : IEventFactory
    {
        public IEvent GetInstantiate(IPiece piece, PieceType pieceType, Coordinate sourceCoordinate)
        {
            return new InstantiateEvent(piece, pieceType, sourceCoordinate);
        }
    }
}