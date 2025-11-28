using System.Collections.Generic;
using Game.Common;
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
        IEvent GetDamagePieceEvent(
            IPiece piece,
            DamagePieceReason damagePieceReason,
            Direction direction,
            DestroyPieceData destroyPieceData
        );

        [NotNull]
        IEvent GetDamagePiecesByLineClearEvent(IEnumerable<IPiece> pieces);

        [NotNull]
        IEvent GetDestroyPieceEvent(
            int pieceId,
            DestroyPieceReason destroyPieceReason,
            DestroyPieceEvent.GoalCurrentAmountUpdatedData goalData,
            DestroyPieceEvent.DecomposePieceData decomposeData
        );

        [NotNull]
        IEvent GetMovePieceEvent(int pieceId, int rowOffset, int columnOffset, MovePieceReason movePieceReason);

        [NotNull]
        IEvent GetMovePiecesByGravityEvent(IEnumerable<KeyValuePair<int, int>> fallData);

        [NotNull]
        IEvent GetMoveCameraEvent(int rowOffset);
    }
}