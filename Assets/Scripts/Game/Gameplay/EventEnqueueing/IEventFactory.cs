using Game.Gameplay.Board;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.EventEnqueueing.Events.Reasons;
using JetBrains.Annotations;

namespace Game.Gameplay.EventEnqueueing
{
    public interface IEventFactory
    {
        [NotNull]
        IEvent GetInstantiate(
            IPiece piece,
            PieceType pieceType,
            Coordinate sourceCoordinate,
            InstantiateReason instantiateReason
        );
    }
}