using System.Collections.Generic;
using Infrastructure.System;
using Infrastructure.System.Exceptions;
using Infrastructure.System.Matrix.Utils;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public abstract class Piece : IPiece
    {
        [NotNull] private const string AliveKey = "ALIVE";
        [NotNull] private const string RotationKey = "ROTATION";

        public int Id { get; }

        public PieceType Type { get; }

        public int Width => GetRotatedGrid().GetLength(1);

        public int Height => GetRotatedGrid().GetLength(0);

        public bool Alive { get; private set; } = true;

        public IEnumerable<KeyValuePair<string, string>> State => GetState();

        public int Rotation
        {
            get => _rotation;
            set
            {
                if (!CanRotate)
                {
                    return;
                }

                value %= MatrixUtils.MaxRotationSteps;

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

        public bool IsFilled(int rowOffset, int columnOffset)
        {
            ArgumentOutOfRangeException.ThrowIfNot(rowOffset, ComparisonOperator.GreaterThanOrEqualTo, 0);
            ArgumentOutOfRangeException.ThrowIfNot(rowOffset, ComparisonOperator.LessThan, Height);
            ArgumentOutOfRangeException.ThrowIfNot(columnOffset, ComparisonOperator.GreaterThanOrEqualTo, 0);
            ArgumentOutOfRangeException.ThrowIfNot(columnOffset, ComparisonOperator.LessThan, Width);

            return GetRotatedGrid()[rowOffset, columnOffset];
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
            if (!IsFilled(rowOffset, columnOffset))
            {
                InvalidOperationException.Throw($"Piece is not filled at offsets (RowOffset: {rowOffset}, ColumnOffset: {columnOffset})");
            }

            // Undo clockwise rotation (by using counterclockwise rotation) in order to provide non-rotated offsets

            MatrixUtils.GetCounterClockwiseRotatedIndices(
                Height,
                Width,
                rowOffset,
                columnOffset,
                out int rotatedRowOffset,
                out int rotatedColumnOffset,
                Rotation
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

        protected void AddStateEntry<T>([NotNull] string key, [NotNull] T value)
        {
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(value);

            if (!_temporaryStateEntries.TryAdd(key, value.ToString()))
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
            return _rotatedGrid ??= GetGrid().RotateClockwise(Rotation);
        }

        [NotNull]
        protected abstract bool[,] GetGrid();
    }
}