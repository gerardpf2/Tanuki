using System.Collections.Generic;
using Game.Gameplay.Board.Utils;
using Game.Gameplay.Pieces.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System.Exceptions;
using Infrastructure.System.Matrix.Utils;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public class Board : IBoard
    {
        [NotNull] private readonly IDictionary<int, IPiece> _piecesByPieceIds = new Dictionary<int, IPiece>();
        [NotNull] private readonly IDictionary<int, Coordinate> _sourceCoordinatesByPieceIds = new Dictionary<int, Coordinate>();
        [NotNull] private readonly SortedList<int, int> _piecesAmountByRows = new();
        private int?[,] _pieceIds;

        public int HighestNonEmptyRow => _piecesAmountByRows.Count > 0 ? _piecesAmountByRows.Keys[^1] : -1;

        public int Columns
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_pieceIds);

                return _pieceIds.GetLength(1);
            }
        }

        public IEnumerable<int> PieceIds => _piecesByPieceIds.Keys;

        private int Rows
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_pieceIds);

                return _pieceIds.GetLength(0);
            }
        }

        public Board(int columns) // TODO: Remove
        {
            Build(columns);
        }

        public void Build(int columns)
        {
            Clear();

            const int rows = 0;

            _pieceIds = new int?[rows, columns];
        }

        public void Clear()
        {
            _piecesByPieceIds.Clear();
            _sourceCoordinatesByPieceIds.Clear();
            _piecesAmountByRows.Clear();
            _pieceIds?.Fill(null);
        }

        public IPiece GetPiece(int pieceId)
        {
            if (!_piecesByPieceIds.TryGetValue(pieceId, out IPiece piece))
            {
                InvalidOperationException.Throw($"Piece with Id: {pieceId} cannot be found");
            }

            InvalidOperationException.ThrowIfNull(piece);

            return piece;
        }

        public Coordinate GetSourceCoordinate(int pieceId)
        {
            if (!_sourceCoordinatesByPieceIds.TryGetValue(pieceId, out Coordinate sourceCoordinate))
            {
                InvalidOperationException.Throw($"Piece with Id: {pieceId} cannot be found");
            }

            return sourceCoordinate;
        }

        public int? GetPieceId(Coordinate coordinate)
        {
            if (!IsInside(coordinate))
            {
                ArgumentOutOfRangeException.Throw(coordinate);
            }

            InvalidOperationException.ThrowIfNull(_pieceIds);

            return _pieceIds[coordinate.Row, coordinate.Column];
        }

        public void AddPiece([NotNull] IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            int pieceId = piece.Id;

            if (_piecesByPieceIds.ContainsKey(pieceId))
            {
                InvalidOperationException.Throw($"Piece with Id: {pieceId} has already been added");
            }

            int newRows = sourceCoordinate.Row + piece.Height;

            ExpandRowsIfNeeded(newRows);

            foreach (Coordinate coordinate in piece.GetCoordinates(sourceCoordinate))
            {
                int? otherPieceId = GetPieceId(coordinate);

                if (otherPieceId.HasValue)
                {
                    InvalidOperationException.Throw($"Coordinate {coordinate} is not empty. It contains piece with Id: {otherPieceId}");
                }

                Set(pieceId, coordinate);
            }

            _piecesByPieceIds.Add(pieceId, piece);
            _sourceCoordinatesByPieceIds.Add(pieceId, sourceCoordinate);
        }

        public void RemovePiece(int pieceId)
        {
            IPiece piece = GetPiece(pieceId);
            Coordinate sourceCoordinate = GetSourceCoordinate(pieceId);

            foreach (Coordinate coordinate in piece.GetCoordinates(sourceCoordinate))
            {
                int? otherPieceId = GetPieceId(coordinate);

                if (otherPieceId != pieceId)
                {
                    InvalidOperationException.Throw($"Coordinate {coordinate} does not contain the expected piece. It should contain piece with Id: {pieceId} but instead it contains piece with Id: {otherPieceId}");
                }

                Set(null, coordinate);
            }

            _piecesByPieceIds.Remove(pieceId);
            _sourceCoordinatesByPieceIds.Remove(pieceId);
        }

        public void MovePiece(int pieceId, int rowOffset, int columnOffset)
        {
            IPiece piece = GetPiece(pieceId);
            Coordinate sourceCoordinate = GetSourceCoordinate(pieceId);

            RemovePiece(pieceId);

            Coordinate newSourceCoordinate = sourceCoordinate.WithOffset(rowOffset, columnOffset);

            AddPiece(piece, newSourceCoordinate);
        }

        private bool IsInside(Coordinate coordinate)
        {
            return
                coordinate.Row >= 0 && coordinate.Row < Rows &&
                coordinate.Column >= 0 && coordinate.Column < Columns;
        }

        private void ExpandRowsIfNeeded(int newRows)
        {
            if (Rows >= newRows)
            {
                return;
            }

            ExpandRows(newRows);
        }

        private void ExpandRows(int newRows)
        {
            InvalidOperationException.ThrowIfNull(_pieceIds);

            int rows = Rows;
            int columns = Columns;

            int?[,] newPieceIds = new int?[newRows, columns];

            for (int row = 0; row < rows; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    newPieceIds[row, column] = _pieceIds[row, column];
                }
            }

            _pieceIds = newPieceIds;
        }

        private void Set(int? pieceId, Coordinate coordinate)
        {
            if (!IsInside(coordinate))
            {
                ArgumentOutOfRangeException.Throw(coordinate);
            }

            InvalidOperationException.ThrowIfNull(_pieceIds);

            UpdatePiecesAmountByRows(coordinate.Row, pieceId.HasValue);

            _pieceIds[coordinate.Row, coordinate.Column] = pieceId;
        }

        private void UpdatePiecesAmountByRows(int updatedRow, bool pieceAdded)
        {
            if (pieceAdded)
            {
                if (!_piecesAmountByRows.TryAdd(updatedRow, 1))
                {
                    ++_piecesAmountByRows[updatedRow];
                }
            }
            else
            {
                if (!_piecesAmountByRows.ContainsKey(updatedRow))
                {
                    InvalidOperationException.Throw($"Row {updatedRow} cannot be found in pieces amount by row");
                }

                int piecesAmount = _piecesAmountByRows[updatedRow];

                if (piecesAmount <= 0)
                {
                    InvalidOperationException.Throw($"Row {updatedRow} cannot contain {piecesAmount} pieces");
                }

                if (piecesAmount == 1)
                {
                    _piecesAmountByRows.Remove(updatedRow);
                }
                else
                {
                    _piecesAmountByRows[updatedRow] = piecesAmount - 1;
                }
            }
        }
    }
}