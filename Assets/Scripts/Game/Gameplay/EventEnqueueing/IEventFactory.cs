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
        IEvent GetInstantiatePieceEvent(
            IPiece piece,
            Coordinate sourceCoordinate,
            InstantiatePieceReason instantiatePieceReason
        );

        [NotNull]
        IEvent GetInstantiatePlayerPieceEvent(IPiece piece);

        [NotNull]
        IEvent GetLockPlayerPieceEvent(IPiece piece, Coordinate lockSourceCoordinate);

        [NotNull]
        IEvent GetDamagePieceEvent(IPiece piece);

        [NotNull]
        IEvent GetDestroyPieceEvent(IPiece piece, DestroyPieceReason destroyPieceReason);

        [NotNull]
        IEvent GetMovePieceEvent(IPiece piece, int rowOffset, int columnOffset, MovePieceReason movePieceReason);
    }
}