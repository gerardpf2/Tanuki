using Game.Gameplay.EventEnqueueing.Events;
using Game.Gameplay.Model.Board.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.EventEnqueueing
{
    public interface IEventFactory
    {
        [NotNull]
        IEvent GetInstantiate(IPiece piece, PieceType pieceType, Coordinate sourceCoordinate);
    }
}