using System;
using System.Collections.Generic;
using Game.Common.Pieces;
using Infrastructure.System;
using Infrastructure.System.Matrix.Utils;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using ArgumentOutOfRangeException = Infrastructure.System.Exceptions.ArgumentOutOfRangeException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Pieces.Pieces
{
    public abstract class Piece : IPiece
    {
        private const bool DefaultAlive = true;
        [NotNull] private const string AliveKey = "ALIVE";
        [NotNull] private const string RotationKey = "ROTATION";

        public int Id { get; }

        public PieceType Type { get; }

        public int Height => Grid.GetLength(0);

        public int Width => Grid.GetLength(1);

        public bool Alive { get; private set; } = DefaultAlive;

        public IEnumerable<KeyValuePair<string, string>> State => GetState();

        public virtual bool CanRotate => true;

        public int Rotation
        {
            get => _rotation;
            set
            {
                if (!CanRotate)
                {
                    InvalidOperationException.Throw($"Piece with Type: {Type} cannot be rotated");
                }

                value %= MatrixUtils.MaxRotationSteps;

                if (Rotation == value)
                {
                    return;
                }

                int steps = value - Rotation;

                _rotation = value;

                Rotate(steps);
            }
        }

        [NotNull] // Get
        private bool[,] Grid
        {
            get => _grid ??= GetGrid();
            set => _grid = value;
        }

        [NotNull] protected readonly IConverter Converter;

        [NotNull] private readonly IDictionary<string, string> _temporaryStateEntries = new Dictionary<string, string>();
        private bool[,] _grid;
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

            return Grid[rowOffset, columnOffset];
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

            return _temporaryStateEntries.Count == 0 ? null : new Dictionary<string, string>(_temporaryStateEntries);
        }

        protected virtual void AddStateEntries()
        {
            AddStateEntry(AliveKey, Alive, DefaultAlive);
            AddStateEntry(RotationKey, Rotation);
        }

        protected void AddStateEntry<T>([NotNull] string key, [NotNull] T value, T defaultValue = default) where T : IEquatable<T>
        {
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(value);

            if (value.Equals(defaultValue))
            {
                return;
            }

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

        private void Rotate(int steps)
        {
            // TODO: Optimize

            if (steps < 0)
            {
                steps += MatrixUtils.MaxRotationSteps;
            }

            Grid = Grid.RotateClockwise(steps);
        }

        protected virtual void HandleDamaged(int nonRotatedRowOffset, int nonRotatedColumnOffset)
        {
            Alive = false;
        }

        [NotNull]
        protected abstract bool[,] GetGrid();
    }
}