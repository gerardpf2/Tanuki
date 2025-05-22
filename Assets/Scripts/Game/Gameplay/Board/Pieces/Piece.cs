using System.Collections.Generic;
using Infrastructure.System.Exceptions;

namespace Game.Gameplay.Board.Pieces
{
    public abstract class Piece : IPiece, IPieceUpdater
    {
        public PieceType Type { get; }

        public bool Alive { get; protected set; } = true;

        public IEnumerable<KeyValuePair<string, object>> CustomData { get; }

        protected Piece(PieceType type)
        {
            Type = type;
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

        protected abstract bool IsInside(int rowOffset, int columnOffset);

        protected virtual void HandleDamaged(int rowOffset, int columnOffset)
        {
            Alive = false;
        }
    }
}