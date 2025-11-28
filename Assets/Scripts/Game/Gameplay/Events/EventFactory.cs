using System.Collections.Generic;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Events;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Events
{
    public class EventFactory : IEventFactory
    {
        public IEvent GetInstantiatePieceEvent(
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate,
            InstantiatePieceReason instantiatePieceReason)
        {
            ArgumentNullException.ThrowIfNull(piece);

            IPiece pieceClone = piece.Clone(); // Clone needed so model and view boards have different piece refs

            return new InstantiatePieceEvent(pieceClone, sourceCoordinate, instantiatePieceReason);
        }

        public IEvent GetInstantiatePlayerPieceEvent(IPiece piece, Coordinate sourceCoordinate)
        {
            return new InstantiatePlayerPieceEvent(piece, sourceCoordinate);
        }

        public IEvent GetLockPlayerPieceEvent(
            [NotNull] IPiece piece,
            Coordinate sourceCoordinate,
            Coordinate lockSourceCoordinate,
            int movesAmount)
        {
            ArgumentNullException.ThrowIfNull(piece);

            IPiece pieceClone = piece.Clone(); // Clone needed so model and view boards have different piece refs

            return new LockPlayerPieceEvent(pieceClone, sourceCoordinate, lockSourceCoordinate, movesAmount);
        }

        public IEvent GetDamagePieceEvent(
            [NotNull] IPiece piece,
            DamagePieceReason damagePieceReason,
            Direction direction,
            DestroyPieceData destroyPieceData)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return new DamagePieceEvent(piece.Id, piece.State, damagePieceReason, direction, destroyPieceData);
        }

        public IEvent GetDamagePiecesByLineClearEvent([NotNull, ItemNotNull] IEnumerable<IPiece> pieces)
        {
            ArgumentNullException.ThrowIfNull(pieces);

            DamagePiecesByLineClearEvent damagePiecesByLineClearEvent = new();

            foreach (IPiece piece in pieces)
            {
                ArgumentNullException.ThrowIfNull(piece);

                damagePiecesByLineClearEvent.Add(piece);
            }

            return damagePiecesByLineClearEvent;
        }

        public IEvent GetDestroyPieceEvent(
            int pieceId,
            DestroyPieceReason destroyPieceReason,
            DestroyPieceEvent.GoalCurrentAmountUpdatedData goalData,
            DestroyPieceEvent.DecomposePieceData decomposeData)
        {
            return new DestroyPieceEvent(pieceId, destroyPieceReason, goalData, decomposeData);
        }

        public IEvent GetMovePieceEvent(int pieceId, int rowOffset, int columnOffset, MovePieceReason movePieceReason)
        {
            return new MovePieceEvent(pieceId, rowOffset, columnOffset, movePieceReason);
        }

        public IEvent GetMovePiecesByGravityEvent([NotNull] IEnumerable<KeyValuePair<int, int>> fallData)
        {
            ArgumentNullException.ThrowIfNull(fallData);

            return new MovePiecesByGravityEvent(fallData);
        }

        public IEvent GetMoveCameraEvent(int rowOffset)
        {
            return new MoveCameraEvent(rowOffset);
        }
    }
}