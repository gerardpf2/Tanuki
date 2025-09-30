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
        public uint Id { get; }

        public PieceType Type { get; }

        public bool Alive { get; private set; } = true;

        public IEnumerable<KeyValuePair<string, string>> CustomData => GetCustomData();

        [NotNull] protected readonly IConverter Converter;

        [NotNull] private readonly IDictionary<string, string> _temporaryCustomDataEntries = new Dictionary<string, string>();

        protected Piece([NotNull] IConverter converter, uint id, PieceType type)
        {
            ArgumentNullException.ThrowIfNull(converter);

            Converter = converter;

            Id = id;
            Type = type;
        }

        public abstract IEnumerable<Coordinate> GetCoordinates(Coordinate sourceCoordinate);

        public void ProcessCustomData(IEnumerable<KeyValuePair<string, string>> customData)
        {
            if (customData is null)
            {
                return;
            }

            foreach ((string key, string value) in customData)
            {
                bool processed = ProcessCustomDataEntry(key, value);

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

        private IEnumerable<KeyValuePair<string, string>> GetCustomData()
        {
            _temporaryCustomDataEntries.Clear();

            AddCustomDataEntries();

            return
                _temporaryCustomDataEntries.Count > 0 ?
                    new Dictionary<string, string>(_temporaryCustomDataEntries) :
                    null; // Avoid serialize when empty
        }

        protected virtual void AddCustomDataEntries() { }

        protected void AddCustomDataEntry<T>([NotNull] string key, T value) where T : IEquatable<T>
        {
            ArgumentNullException.ThrowIfNull(key);

            if (value is null || value.Equals(default))
            {
                return; // Avoid serialize when null or default
            }

            _temporaryCustomDataEntries.Add(key, Converter.Convert<string>(value));
        }

        protected virtual bool ProcessCustomDataEntry(string key, string value)
        {
            return false;
        }

        protected abstract bool IsInside(int rowOffset, int columnOffset);

        protected virtual void HandleDamaged(int rowOffset, int columnOffset)
        {
            Alive = false;
        }
    }
}