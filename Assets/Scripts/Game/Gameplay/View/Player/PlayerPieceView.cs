using System;
using Game.Common.Pieces;
using Game.Gameplay.Board;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.View.Pieces;
using Game.Gameplay.View.Pieces.Pieces;
using Infrastructure.Unity.Pooling;
using JetBrains.Annotations;
using UnityEngine;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.View.Player
{
    public class PlayerPieceView : BasePlayerPieceView, IPlayerPieceView
    {
        [NotNull] private readonly IBoard _board;

        public event Action OnMoved;
        public event Action OnRotated;

        protected override string ParentName => "PlayerPieceParent";

        public PlayerPieceView(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            [NotNull] IGameObjectPool gameObjectPool,
            [NotNull] IBoard board) : base(pieceViewDefinitionGetter, gameObjectPool)
        {
            ArgumentNullException.ThrowIfNull(board);

            _board = board;
        }

        public void Instantiate([NotNull] IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            InstantiateImpl(piece, sourceCoordinate);
        }

        public bool CanMove(int columnOffset)
        {
            int column = Coordinate.Column + columnOffset;
            int clampedColumn = GetClampedColumn(Piece, column);

            return column == clampedColumn;
        }

        public void Move(int columnOffset)
        {
            const int rowOffset = 0;

            if (!CanMove(columnOffset))
            {
                InvalidOperationException.Throw("Cannot be moved");
            }

            Coordinate = Coordinate.WithOffset(rowOffset, columnOffset);

            OnMoved?.Invoke();
        }

        public bool CanRotate()
        {
            return Piece.CanRotate;
        }

        public void Rotate()
        {
            if (!CanRotate())
            {
                InvalidOperationException.Throw("Cannot be rotated");
            }

            RotateAndUpdateCoordinate();
            NotifyRotated();

            OnMoved?.Invoke();
            OnRotated?.Invoke();

            return;

            void RotateAndUpdateCoordinate()
            {
                IPiece piece = Piece;

                int height = piece.Height;
                int width = piece.Width;

                ++piece.Rotation;

                int heightAfterRotate = piece.Height;
                int widthAfterRotate = piece.Width;

                int rowOffset = height - heightAfterRotate;
                int columnOffset = (width - widthAfterRotate) / 2;

                Coordinate coordinate = Coordinate;

                int row = coordinate.Row + rowOffset;
                int column = coordinate.Column + columnOffset;

                Coordinate = new Coordinate(row, GetClampedColumn(piece, column));
            }

            void NotifyRotated()
            {
                GameObject instance = Instance;

                InvalidOperationException.ThrowIfNull(instance);

                IPieceViewEventNotifier pieceViewEventNotifier = instance.GetComponent<IPieceViewEventNotifier>();

                InvalidOperationException.ThrowIfNull(pieceViewEventNotifier);

                pieceViewEventNotifier.OnRotated();
            }
        }

        protected override IPieceViewDefinition GetPieceViewDefinition(
            [NotNull] IPieceViewDefinitionGetter pieceViewDefinitionGetter,
            PieceType pieceType)
        {
            ArgumentNullException.ThrowIfNull(pieceViewDefinitionGetter);

            return pieceViewDefinitionGetter.Get(pieceType);
        }

        private int GetClampedColumn([NotNull] IPiece piece, int column)
        {
            ArgumentNullException.ThrowIfNull(piece);

            const int minColumn = 0;
            int maxColumn = Math.Max(_board.Columns - piece.Width, minColumn);

            return Math.Clamp(column, minColumn, maxColumn);
        }
    }
}