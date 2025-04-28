using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.Model.Board.Pieces;

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