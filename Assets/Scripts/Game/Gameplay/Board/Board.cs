using System;
using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Board
{
    public class Board : IBoard
    {
        [NotNull] private readonly IPieceCachedPropertiesGetter _pieceCachedPropertiesGetter;

        [NotNull] private readonly SortedList<int, int> _piecesPerRowSorted = new();
        private IPiece[,] _pieces;

        private int _highestNonEmptyRow;

        public event Action OnHighestNonEmptyRowUpdated;

        public int Rows
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_pieces);

                return _pieces.GetLength(0);
            }
        }

        public int Columns
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_pieces);

                return _pieces.GetLength(1);
            }
        }

        public int HighestNonEmptyRow
        {
            get => _highestNonEmptyRow;
            private set
            {
                if (HighestNonEmptyRow == value)
                {
                    return;
                }

                _highestNonEmptyRow = value;

                OnHighestNonEmptyRowUpdated?.Invoke();
            }
        }

        public IDictionary<IPiece, Coordinate> PieceSourceCoordinates { get; } = new Dictionary<IPiece, Coordinate>();

        public Board([NotNull] IPieceCachedPropertiesGetter pieceCachedPropertiesGetter, int rows, int columns)
        {
            ArgumentNullException.ThrowIfNull(pieceCachedPropertiesGetter);

            _pieceCachedPropertiesGetter = pieceCachedPropertiesGetter;

            _pieces = new IPiece[rows, columns];
        }

        public IPiece Get(Coordinate coordinate)
        {
            if (!this.IsInside(coordinate))
            {
                ArgumentOutOfRangeException.Throw(coordinate);
            }

            InvalidOperationException.ThrowIfNull(_pieces);

            return _pieces[coordinate.Row, coordinate.Column];
        }

        public void Add([NotNull] IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (PieceSourceCoordinates.ContainsKey(piece))
            {
                InvalidOperationException.Throw("Piece has already been added");
            }

            PieceSourceCoordinates.Add(piece, sourceCoordinate);

            ExpandRowsIfNeeded(piece, sourceCoordinate.Row);

            foreach (Coordinate coordinate in piece.GetCoordinates(sourceCoordinate))
            {
                if (Get(coordinate) is not null)
                {
                    InvalidOperationException.Throw($"Coordinate {coordinate} is not empty");
                }

                Set(piece, coordinate);
            }
        }

        public void Remove([NotNull] IPiece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (!PieceSourceCoordinates.TryGetValue(piece, out Coordinate sourceCoordinate))
            {
                InvalidOperationException.Throw("Piece cannot be found");
            }

            PieceSourceCoordinates.Remove(piece);

            foreach (Coordinate coordinate in piece.GetCoordinates(sourceCoordinate))
            {
                if (Get(coordinate) != piece)
                {
                    InvalidOperationException.Throw($"Coordinate {coordinate} does not contain the expected piece");
                }

                Set(null, coordinate);
            }
        }

        public void Move([NotNull] IPiece piece, int rowOffset, int columnOffset)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (!PieceSourceCoordinates.TryGetValue(piece, out Coordinate sourceCoordinate))
            {
                InvalidOperationException.Throw("Piece cannot be found");
            }

            Remove(piece);

            Coordinate newSourceCoordinate = sourceCoordinate.WithOffset(rowOffset, columnOffset);

            Add(piece, newSourceCoordinate);
        }

        private void ExpandRowsIfNeeded([NotNull] IPiece piece, int row)
        {
            ArgumentNullException.ThrowIfNull(piece);

            int newRows = row + _pieceCachedPropertiesGetter.GetTopMostRowOffset(piece) + 1;

            if (Rows >= newRows)
            {
                return;
            }

            ExpandRows(newRows);
        }

        private void ExpandRows(int newRows)
        {
            InvalidOperationException.ThrowIfNull(_pieces);

            int rows = Rows;
            int columns = Columns;

            IPiece[,] newPieces = new IPiece[newRows, columns];

            for (int row = 0; row < rows; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    newPieces[row, column] = _pieces[row, column];
                }
            }

            _pieces = newPieces;
        }

        private void Set(IPiece piece, Coordinate coordinate)
        {
            if (!this.IsInside(coordinate))
            {
                ArgumentOutOfRangeException.Throw(coordinate);
            }

            InvalidOperationException.ThrowIfNull(_pieces);

            _pieces[coordinate.Row, coordinate.Column] = piece;

            UpdatePiecesPerRowSorted(coordinate.Row, piece is not null);

            HighestNonEmptyRow = _piecesPerRowSorted.Count > 0 ? _piecesPerRowSorted.Keys[^1] : 0;
        }

        private void UpdatePiecesPerRowSorted(int updatedRow, bool pieceAdded)
        {
            if (pieceAdded)
            {
                if (!_piecesPerRowSorted.TryAdd(updatedRow, 1))
                {
                    ++_piecesPerRowSorted[updatedRow];
                }
            }
            else
            {
                if (!_piecesPerRowSorted.ContainsKey(updatedRow))
                {
                    InvalidOperationException.Throw($"Row {updatedRow} cannot be found in pieces per row");
                }

                int piecesRow = _piecesPerRowSorted[updatedRow];

                if (piecesRow <= 0)
                {
                    InvalidOperationException.Throw($"Row {updatedRow} cannot contain {piecesRow} pieces");
                }

                if (piecesRow == 1)
                {
                    _piecesPerRowSorted.Remove(updatedRow);
                }
                else
                {
                    _piecesPerRowSorted[updatedRow] = piecesRow - 1;
                }
            }
        }
    }
}