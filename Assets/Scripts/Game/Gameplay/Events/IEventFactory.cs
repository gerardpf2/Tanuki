using System.Collections.Generic;
using Game.Common;
using Game.Common.Pieces;
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
        InstantiatePieceEvent GetInstantiatePieceEvent(
            IPiece piece,
            Coordinate sourceCoordinate,
            InstantiatePieceReason instantiatePieceReason
        );

        [NotNull]
        IEvent GetInstantiatePlayerPieceEvent(IPiece piece, Coordinate sourceCoordinate);

        [NotNull]
        IEvent GetLockPlayerPieceEvent(
            InstantiatePieceEvent instantiatePieceEvent,
            Coordinate sourceCoordinate,
            int movesAmount
        );

        [NotNull]
        IEvent GetDamagePieceEvent(
            DestroyPieceEvent destroyPieceEvent,
            IPiece piece,
            DamagePieceReason damagePieceReason,
            Direction direction
        );

        [NotNull]
        IEvent GetDamagePiecesByLineClearEvent(IEnumerable<IPiece> pieces);

        [NotNull]
        DestroyPieceEvent GetDestroyPieceEvent(
            UpdateGoalEvent updateGoalEvent,
            int pieceId,
            DestroyPieceReason destroyPieceReason,
            DecomposePieceData decomposePieceData
        );

        [NotNull]
        IEvent GetMovePieceEvent(int pieceId, int rowOffset, int columnOffset, MovePieceReason movePieceReason);

        [NotNull]
        IEvent GetMovePiecesByGravityEvent(IEnumerable<KeyValuePair<int, int>> fallData);

        [NotNull]
        IEvent GetMoveCameraEvent(int rowOffset);

        [NotNull]
        UpdateGoalEvent GetUpdateGoalEvent(PieceType pieceType, int currentAmount, Coordinate coordinate);
    }
}