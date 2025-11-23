using System;
using Game.Gameplay.Board;
using Game.Gameplay.Events.Reasons;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Animation.Movement;
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
        private readonly int _rowOffset;
        private readonly int _columnOffset;
        private readonly MovePieceReason _movePieceReason;

        protected BaseMovePieceAction(
            [NotNull] IBoard board,
            [NotNull] IMovementHelper movementHelper,
            int rowOffset,
            int columnOffset,
            MovePieceReason movePieceReason)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(movePieceReason);

            _board = board;
            _movementHelper = movementHelper;
            _rowOffset = rowOffset;
            _columnOffset = columnOffset;
            _movePieceReason = movePieceReason;
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
        protected abstract GameObject GetPieceInstance();

        protected abstract void MovePiece(int rowOffset, int columnOffset);

        [NotNull]
        protected abstract IPiece GetPiece();

        protected abstract Coordinate GetSourceCoordinate();

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

            // TODO
        }
    }
}