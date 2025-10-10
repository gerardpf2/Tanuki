using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;
using Game.Gameplay.Board.Utils;
using Infrastructure.System.Exceptions;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public class Board : IBoard
    {
        [NotNull] private readonly IPieceCachedPropertiesGetter _pieceCachedPropertiesGetter;

        [NotNull] private readonly IDictionary<int, IPiece> _piecesByIds = new Dictionary<int, IPiece>();
        [NotNull] private readonly IDictionary<int, Coordinate> _sourceCoordinatesByIds = new Dictionary<int, Coordinate>();
        [NotNull] private readonly SortedList<int, int> _piecesAmountByRows = new();
        private int?[,] _ids;

        public int Rows
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_ids);

                return _ids.GetLength(0);
            }
        }

        public int Columns
        {
            get
            {
                InvalidOperationException.ThrowIfNull(_ids);

                return _ids.GetLength(1);
            }
        }

        public int HighestNonEmptyRow => _piecesAmountByRows.Count > 0 ? _piecesAmountByRows.Keys[^1] : 0;

        public IEnumerable<int> Ids => _piecesByIds.Keys;

        public Board([NotNull] IPieceCachedPropertiesGetter pieceCachedPropertiesGetter, int rows, int columns)
        {
            ArgumentNullException.ThrowIfNull(pieceCachedPropertiesGetter);

            _pieceCachedPropertiesGetter = pieceCachedPropertiesGetter;

            _ids = new int?[rows, columns];
        }

        public IPiece Get(int id)
        {
            if (!_piecesByIds.TryGetValue(id, out IPiece piece))
            {
                InvalidOperationException.Throw($"Piece with Id: {id} cannot be found");
            }

            InvalidOperationException.ThrowIfNull(piece);

            return piece;
        }

        public Coordinate GetSourceCoordinate(int id)
        {
            if (!_sourceCoordinatesByIds.TryGetValue(id, out Coordinate sourceCoordinate))
            {
                InvalidOperationException.Throw($"Piece with Id: {id} cannot be found");
            }

            return sourceCoordinate;
        }

        public int? Get(Coordinate coordinate)
        {
            if (!this.IsInside(coordinate))
            {
                ArgumentOutOfRangeException.Throw(coordinate);
            }

            InvalidOperationException.ThrowIfNull(_ids);

            return _ids[coordinate.Row, coordinate.Column];
        }

        public void Add([NotNull] IPiece piece, Coordinate sourceCoordinate)
        {
            ArgumentNullException.ThrowIfNull(piece);

            int id = piece.Id;

            if (_piecesByIds.ContainsKey(id))
            {
                InvalidOperationException.Throw($"Piece with Id: {id} has already been added");
            }

            int newRows = sourceCoordinate.Row + _pieceCachedPropertiesGetter.GetTopMostRowOffset(piece) + 1;

            ExpandRowsIfNeeded(newRows);

            foreach (Coordinate coordinate in piece.GetCoordinates(sourceCoordinate))
            {
                int? otherId = Get(coordinate);

                if (otherId.HasValue)
                {
                    InvalidOperationException.Throw($"Coordinate {coordinate} is not empty. It contains piece with Id: {otherId}");
                }

                Set(id, coordinate);
            }

            _piecesByIds.Add(id, piece);
            _sourceCoordinatesByIds.Add(id, sourceCoordinate);
        }

        public void Remove(int id)
        {
            IPiece piece = Get(id);
            Coordinate sourceCoordinate = GetSourceCoordinate(id);

            foreach (Coordinate coordinate in piece.GetCoordinates(sourceCoordinate))
            {
                int? otherId = Get(coordinate);

                if (otherId != id)
                {
                    InvalidOperationException.Throw($"Coordinate {coordinate} does not contain the expected piece. It should contain piece with Id: {id} but instead it contains piece with Id: {otherId}");
                }

                Set(null, coordinate);
            }

            _piecesByIds.Remove(id);
            _sourceCoordinatesByIds.Remove(id);
        }

        public void Move(int id, int rowOffset, int columnOffset)
        {
            IPiece piece = Get(id);
            Coordinate sourceCoordinate = GetSourceCoordinate(id);

            Remove(id);

            Coordinate newSourceCoordinate = sourceCoordinate.WithOffset(rowOffset, columnOffset);

            Add(piece, newSourceCoordinate);
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
            InvalidOperationException.ThrowIfNull(_ids);

            int rows = Rows;
            int columns = Columns;

            int?[,] newIds = new int?[newRows, columns];

            for (int row = 0; row < rows; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    newIds[row, column] = _ids[row, column];
                }
            }

            _ids = newIds;
        }

        private void Set(int? id, Coordinate coordinate)
        {
            if (!this.IsInside(coordinate))
            {
                ArgumentOutOfRangeException.Throw(coordinate);
            }

            InvalidOperationException.ThrowIfNull(_ids);

            UpdatePiecesAmountByRows(coordinate.Row, id.HasValue);

            _ids[coordinate.Row, coordinate.Column] = id;
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