using System.Collections.Generic;
using Infrastructure.System.Exceptions;

namespace Game.Gameplay.Board.Pieces
{
    public abstract class Piece : IPiece, IPieceUpdater
    {
        public PieceType Type { get; }

        public bool Alive { get; protected set; } = true;

        public virtual IEnumerable<KeyValuePair<string, object>> CustomData => null;

        protected Piece(PieceType type, IEnumerable<KeyValuePair<string, object>> customData)
        {
            Type = type;

            ProcessCustomData(customData);
        }

        public abstract IEnumerable<Coordinate> GetCoordinates(Coordinate sourceCoordinate);

        public void Damage(int rowOffset, int columnOffset)
        {
            if (!IsInside(rowOffset, columnOffset))
            {
                InvalidOperationException.Throw($"Offsets (RowOffset: {rowOffset}, ColumnOffset: {columnOffset}) are not inside");
            }

            HandleDamaged(rowOffset, columnOffset);
        }

        private void ProcessCustomData(IEnumerable<KeyValuePair<string, object>> customData)
        {
            if (customData is null)
            {
                return;
            }

            foreach ((string key, object value) in customData)
            {
                bool processed = ProcessCustomDataEntry(key, value);

                if (!processed)
                {
                    InvalidOperationException.Throw($"Custom data entry with Key: {key} and Value: {value} cannot be processed");
                }
            }
        }

        protected virtual bool ProcessCustomDataEntry(string key, object value)
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