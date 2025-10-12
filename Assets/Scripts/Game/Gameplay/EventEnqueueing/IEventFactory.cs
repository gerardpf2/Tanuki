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
        IEvent GetDestroyPieceEvent(int pieceId, DestroyPieceReason destroyPieceReason);

        [NotNull]
        IEvent GetMovePieceEvent(int pieceId, int rowOffset, int columnOffset, MovePieceReason movePieceReason);

        [NotNull]
        IEvent GetSetCameraRowEvent(int topRow);

        [NotNull]
        IEvent GetSetGoalCurrentAmountEvent(PieceType pieceType, int currentAmount, Coordinate coordinate);
    }
}