using System;

namespace Game.Gameplay.Board
{
    public readonly struct Coordinate : IEquatable<Coordinate>
    {
        public readonly int Row;
        public readonly int Column;

        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return $"(Row: {Row}, Column: {Column})";
        }

        public bool Equals(Coordinate other)
        {
            return Row == other.Row && Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinate other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
    }
}