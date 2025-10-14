using System;
using System.Collections.Generic;
using Game.Gameplay.Board.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Board.Pieces
{
    public abstract class Piece : IPiece
    {
        [NotNull] private const string AliveKey = "ALIVE";
        [NotNull] private const string RotationKey = "ROTATION";

        public int Id { get; }

        public PieceType Type { get; }

        public bool Alive { get; private set; } = true;

        public IEnumerable<KeyValuePair<string, string>> State => GetState();

        public bool[,] Grid => _rotatedGrid ??= GetRotatedGrid();

        public int Rotation
        {
            get => _rotation;
            set
            {
                if (!CanRotate)
                {
                    return;
                }

                value %= 4; // TODO: Const

                if (Rotation == value)
                {
                    return;
                }

                _rotatedGrid = null;
                _rotation = value;
            }
        }

        protected virtual bool CanRotate => true;

        [NotNull] protected readonly IConverter Converter;

        [NotNull] private readonly IDictionary<string, string> _temporaryStateEntries = new Dictionary<string, string>();

        private bool[,] _rotatedGrid;
        private int _rotation;

        protected Piece([NotNull] IConverter converter, int id, PieceType type)
        {
            ArgumentNullException.ThrowIfNull(converter);

            Converter = converter;

            Id = id;
            Type = type;
        }

        public void ProcessState(IEnumerable<KeyValuePair<string, string>> state)
        {
            if (state is null)
            {
                return;
            }

            foreach ((string key, string value) in state)
            {
                bool processed = ProcessStateEntry(key, value);

                if (!processed)
                {
                    InvalidOperationException.Throw($"State entry with Key: {key} and Value: {value} cannot be processed");
                }
            }
        }

        public void Damage(int rowOffset, int columnOffset)
        {
            if (!this.IsFilled(rowOffset, columnOffset))
            {
                InvalidOperationException.Throw($"Piece is not filled at offsets (RowOffset: {rowOffset}, ColumnOffset: {columnOffset})");
            }

            // Undo clockwise rotation (by using counter clockwise rotation) in order to provide non rotated offsets

            bool[,] grid = Grid;

            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);
            int steps = Rotation;

            GetCounterClockwiseRotatedIndices(
                rows,
                columns,
                rowOffset,
                columnOffset,
                out int rotatedRowOffset,
                out int rotatedColumnOffset,
                steps
            );

            HandleDamaged(rotatedRowOffset, rotatedColumnOffset);
        }

        public abstract IPiece Clone();

        private IEnumerable<KeyValuePair<string, string>> GetState()
        {
            _temporaryStateEntries.Clear();

            AddStateEntries();

            return new Dictionary<string, string>(_temporaryStateEntries);
        }

        protected virtual void AddStateEntries()
        {
            AddStateEntry(AliveKey, Alive);
            AddStateEntry(RotationKey, Rotation);
        }

        protected void AddStateEntry<T>([NotNull] string key, T value) where T : IEquatable<T>
        {
            ArgumentNullException.ThrowIfNull(key);

            if (!_temporaryStateEntries.TryAdd(key, Converter.Convert<string>(value)))
            {
                InvalidOperationException.Throw($"State entry with Key: {key} and Value: {value} cannot be added");
            }
        }

        protected virtual bool ProcessStateEntry(string key, string value)
        {
            switch (key)
            {
                case AliveKey:
                {
                    Alive = Converter.Convert<bool>(value);

                    return true;
                }
                case RotationKey:
                {
                    Rotation = Converter.Convert<int>(value);

                    return true;
                }
            }

            return false;
        }

        protected virtual void HandleDamaged(int nonRotatedRowOffset, int nonRotatedColumnOffset)
        {
            Alive = false;
        }

        [NotNull]
        private bool[,] GetRotatedGrid()
        {
            return RotateClockwise(GetGrid(), Rotation);
        }

        [NotNull]
        protected abstract bool[,] GetGrid();

        // TODO: Move to infrastructure matrix utils ¿?

        [NotNull]
        private static T[,] RotateClockwise<T>([NotNull] T[,] grid, int steps)
        {
            ArgumentNullException.ThrowIfNull(grid);

            steps %= 4; // TODO: Const

            for (int step = 0; step < steps; ++step)
            {
                grid = RotateClockwise(grid);
            }

            return grid;
        }

        [NotNull]
        private static T[,] RotateClockwise<T>([NotNull] T[,] grid)
        {
            ArgumentNullException.ThrowIfNull(grid);

            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);

            T[,] newGrid = new T[columns, rows];

            for (int row = 0; row < rows; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    GetClockwiseRotatedIndices(columns, row, column, out int rotatedRow, out int rotatedColumn);

                    newGrid[rotatedRow, rotatedColumn] = grid[row, column];
                }
            }

            return newGrid;
        }

        private static void GetClockwiseRotatedIndices(
            int columns,
            int row,
            int column,
            out int rotatedRow,
            out int rotatedColumn)
        {
            rotatedRow = columns - column - 1;
            rotatedColumn = row;
        }

        private static void GetCounterClockwiseRotatedIndices(
            int rows,
            int columns,
            int row,
            int column,
            out int rotatedRow,
            out int rotatedColumn,
            int steps)
        {
            rotatedRow = row;
            rotatedColumn = column;

            steps %= 4; // TODO: Const

            for (int step = 0; step < steps; ++step)
            {
                int rowsTarget = step % 2 == 0 ? rows : columns;

                GetCounterClockwiseRotatedIndices(rowsTarget, row, column, out rotatedRow, out rotatedColumn);

                row = rotatedRow;
                column = rotatedColumn;
            }
        }

        private static void GetCounterClockwiseRotatedIndices(
            int rows,
            int row,
            int column,
            out int rotatedRow,
            out int rotatedColumn)
        {
            rotatedRow = column;
            rotatedColumn = rows - row - 1;
        }
    }
}