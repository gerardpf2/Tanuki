using System.Collections.Generic;
using Infrastructure.System.Exceptions;

namespace Game.Gameplay.Board.Pieces
{
    public abstract class Piece : IPiece, IPieceUpdater
    {
        public PieceType Type { get; }

        public bool Alive { get; protected set; } = true;

        public virtual IEnumerable<KeyValuePair<string, string>> CustomData => null;

        protected Piece(PieceType type)
        {
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