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
            PieceType pieceType,
            Coordinate sourceCoordinate,
            InstantiatePieceReason instantiatePieceReason)
        {
            return new InstantiatePieceEvent(piece, pieceType, sourceCoordinate, instantiatePieceReason);
        }

        public IEvent GetInstantiatePlayerPieceEvent(IPiece piece, PieceType pieceType)
        {
            return new InstantiatePlayerPieceEvent(piece, pieceType);
        }
    }
}