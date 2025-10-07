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
        IEvent GetDamagePieceEvent(IPiece piece, DamagePieceReason damagePieceReason);

        [NotNull]
        IEvent GetDestroyPieceEvent(int id, DestroyPieceReason destroyPieceReason);

        [NotNull]
        IEvent GetMovePieceEvent(int id, int rowOffset, int columnOffset, MovePieceReason movePieceReason);

        [NotNull]
        IEvent GetSetCameraRowEvent(int topRow);
    }
}