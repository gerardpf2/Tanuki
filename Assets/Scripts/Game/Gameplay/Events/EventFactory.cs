using System.Collections.Generic;
using Game.Common;
using Game.Common.Pieces;
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
        public InstantiatePieceEvent GetInstantiatePieceEvent(
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
            InstantiatePieceEvent instantiatePieceEvent,
            Coordinate sourceCoordinate,
            int movesAmount)
        {
            return new LockPlayerPieceEvent(instantiatePieceEvent, sourceCoordinate, movesAmount);
        }

        public IEvent GetDamagePieceEvent(
            DestroyPieceEvent destroyPieceEvent,
            [NotNull] IPiece piece,
            DamagePieceReason damagePieceReason,
            Direction direction)
        {
            ArgumentNullException.ThrowIfNull(piece);

            return new DamagePieceEvent(destroyPieceEvent, piece.Id, piece.State, damagePieceReason, direction);
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

        public DestroyPieceEvent GetDestroyPieceEvent(
            UpdateGoalEvent updateGoalEvent,
            IReadOnlyCollection<InstantiatePieceEvent> instantiatePieceEventsDecompose,
            int pieceId,
            DestroyPieceReason destroyPieceReason)
        {
            return new DestroyPieceEvent(updateGoalEvent, instantiatePieceEventsDecompose, pieceId, destroyPieceReason);
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

        public UpdateGoalEvent GetUpdateGoalEvent(PieceType pieceType, int currentAmount, Coordinate coordinate)
        {
            return new UpdateGoalEvent(pieceType, currentAmount, coordinate);
        }
    }
}