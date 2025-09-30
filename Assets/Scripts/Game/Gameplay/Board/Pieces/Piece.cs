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
        private const string AliveKey = "A";

        public uint Id { get; }

        public PieceType Type { get; }

        public bool Alive { get; private set; } = true;

        public IEnumerable<KeyValuePair<string, string>> State => GetState();

        [NotNull] protected readonly IConverter Converter;

        [NotNull] private readonly IDictionary<string, string> _temporaryStateEntries = new Dictionary<string, string>();

        protected Piece([NotNull] IConverter converter, uint id, PieceType type)
        {
            ArgumentNullException.ThrowIfNull(converter);

            Converter = converter;

            Id = id;
            Type = type;
        }

        public abstract IEnumerable<Coordinate> GetCoordinates(Coordinate sourceCoordinate);

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
                    InvalidOperationException.Throw($"Custom data entry with Key: {key} and Value: {value} cannot be processed");
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

            return
                _temporaryStateEntries.Count > 0 ?
                    new Dictionary<string, string>(_temporaryStateEntries) :
                    null; // Avoid serialize when empty
        }

        protected virtual void AddStateEntries()
        {
            AddStateEntry(AliveKey, Alive);
        }

        protected void AddStateEntry<T>([NotNull] string key, T value) where T : IEquatable<T>
        {
            ArgumentNullException.ThrowIfNull(key);

            if (value is null || value.Equals(default))
            {
                return; // Avoid serialize when null or default
            }

            _temporaryStateEntries.Add(key, Converter.Convert<string>(value));
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
    }
}