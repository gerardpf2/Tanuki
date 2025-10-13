using System;
using System.Collections.Generic;
using Infrastructure.System;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;
using InvalidOperationException = Infrastructure.System.Exceptions.InvalidOperationException;

namespace Game.Gameplay.Board.Pieces
{
    public abstract class Piece : IPiece
    {
        [NotNull] private const string AliveKey = "ALIVE";

        public int Id { get; }

        public PieceType Type { get; }

        public bool Alive { get; private set; } = true;

        public IEnumerable<KeyValuePair<string, string>> State => GetState();

        public bool[,] Grid => _grid ??= GetGrid();

        [NotNull] protected readonly IConverter Converter;

        [NotNull] private readonly IDictionary<string, string> _temporaryStateEntries = new Dictionary<string, string>();

        private bool[,] _grid;

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
            if (!IsInside(rowOffset, columnOffset))
            {
                InvalidOperationException.Throw($"Offsets (RowOffset: {rowOffset}, ColumnOffset: {columnOffset}) are not inside");
            }

            HandleDamaged(rowOffset, columnOffset);
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
            }

            return false;
        }

        protected abstract bool IsInside(int rowOffset, int columnOffset);

        protected virtual void HandleDamaged(int rowOffset, int columnOffset)
        {
            Alive = false;
        }

        [NotNull]
        protected abstract bool[,] GetGrid();
    }
}