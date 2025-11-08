using Game.Gameplay.Board;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using JetBrains.Annotations;

namespace Game.Gameplay.Events
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
        IEvent GetInstantiatePlayerPieceEvent(IPiece piece, Coordinate sourceCoordinate);

        [NotNull]
        IEvent GetLockPlayerPieceEvent(
            IPiece piece,
            Coordinate sourceCoordinate,
            Coordinate lockSourceCoordinate,
            int movesAmount
        );

        [NotNull]
        IEvent GetDamagePieceEvent(IPiece piece, DamagePieceReason damagePieceReason);

        [NotNull]
        IEvent GetDestroyPieceEvent(
            int pieceId,
            DestroyPieceReason destroyPieceReason,
            DestroyPieceEvent.GoalCurrentAmountUpdatedData goalData
        );

        [NotNull]
        IEvent GetMovePieceEvent(int pieceId, int rowOffset, int columnOffset, MovePieceReason movePieceReason);

        [NotNull]
        MovePiecesByGravityEvent GetMovePiecesByGravityEvent();

        [NotNull]
        IEvent GetMoveCameraEvent(int rowOffset);
    }
}