using System;
using System.Collections.Generic;
using Game.Common;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Events.Reasons.Utils;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Animation.Movement;
using Game.Gameplay.View.Board;
using Game.Gameplay.View.Pieces.Pieces;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Actions.Actions
{
    public abstract class BaseMovePieceAction : IAction
    {
        [NotNull] private readonly IBoard _board;
        [NotNull] private readonly IMovementHelper _movementHelper;
        [NotNull] private readonly IBoardView _boardView;
        private readonly int _rowOffset;
        private readonly int _columnOffset;
        private readonly MovePieceReason _movePieceReason;
        private readonly HitPieceReason _hitPieceReason;

        protected BaseMovePieceAction(
            [NotNull] IBoard board,
            [NotNull] IMovementHelper movementHelper,
            [NotNull] IBoardView boardView,
            int rowOffset,
            int columnOffset,
            MovePieceReason movePieceReason)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(movementHelper);
            ArgumentNullException.ThrowIfNull(boardView);

            _board = board;
            _movementHelper = movementHelper;
            _boardView = boardView;
            _rowOffset = rowOffset;
            _columnOffset = columnOffset;
            _movePieceReason = movePieceReason;
            _hitPieceReason = HitPieceReasonUtils.GetFrom(_movePieceReason);
        }

        public void Resolve(Action onComplete)
        {
            GameObject pieceInstance = GetPieceInstance();

            IBoardPieceViewEventNotifier boardPieceViewEventNotifier = pieceInstance.GetComponent<IBoardPieceViewEventNotifier>();

            InvalidOperationException.ThrowIfNull(boardPieceViewEventNotifier);

            boardPieceViewEventNotifier.OnStartMovement(_movePieceReason, OnStartMovementComplete);

            return;

            void OnStartMovementComplete()
            {
                MovePiece(_rowOffset, _columnOffset);

                DoMovement(pieceInstance.transform, OnMovementComplete);
            }

            void OnMovementComplete()
            {
                NotifyHit();

                boardPieceViewEventNotifier.OnEndMovement(_movePieceReason, onComplete);
            }
        }

        [NotNull]
        protected abstract IPiece GetPiece();

        protected abstract Coordinate GetSourceCoordinate();

        [NotNull]
        protected abstract GameObject GetPieceInstance();

        protected abstract void MovePiece(int rowOffset, int columnOffset);

        private void DoMovement(Transform transform, Action onComplete)
        {
            switch (_movePieceReason)
            {
                case MovePieceReason.Gravity:
                    _movementHelper.DoGravityMovement(transform, _rowOffset, _columnOffset, onComplete);
                    break;
                case MovePieceReason.Lock:
                    _movementHelper.DoLockMovement(transform, _rowOffset, _columnOffset, onComplete);
                    break;
                default:
                    ArgumentOutOfRangeException.Throw(_movePieceReason);
                    return;
            }
        }

        private void NotifyHit()
        {
            IPiece piece = GetPiece();
            Coordinate sourceCoordinate = GetSourceCoordinate();

            ICollection<int> visitedPieceIds = new HashSet<int>();

            IEnumerable<int> pieceIdsInContactDown = _board.GetDistinctPieceIdsInContactDown(piece, sourceCoordinate);
            IEnumerable<int> pieceIdsInContactRight = _board.GetDistinctPieceIdsInContactRight(piece, sourceCoordinate);
            IEnumerable<int> pieceIdsInContactLeft = _board.GetDistinctPieceIdsInContactLeft(piece, sourceCoordinate);

            /*
             *
             * A piece can be hit in multiple directions, but only one hit event has to be sent. Then, some priorities
             * need to be defined. Vertical direction hits have more priority that horizontal direction hits. Starting
             * with piece ids in contact down combined with the use of visited piece ids set should guarantee this
             *
             */

            Notify(pieceIdsInContactDown, Direction.Down);
            Notify(pieceIdsInContactRight, Direction.Right);
            Notify(pieceIdsInContactLeft, Direction.Left);

            return;

            void Notify([NotNull] IEnumerable<int> pieceIds, Direction direction)
            {
                ArgumentNullException.ThrowIfNull(pieceIds);

                foreach (int pieceId in pieceIds)
                {
                    if (visitedPieceIds.Contains(pieceId))
                    {
                        continue;
                    }

                    visitedPieceIds.Add(pieceId);

                    GameObject pieceInstance = _boardView.GetPieceInstance(pieceId);

                    IBoardPieceViewEventNotifier boardPieceViewEventNotifier = pieceInstance.GetComponent<IBoardPieceViewEventNotifier>();

                    InvalidOperationException.ThrowIfNull(boardPieceViewEventNotifier);

                    boardPieceViewEventNotifier.OnHit(_hitPieceReason, direction);
                }
            }
        }
    }
}