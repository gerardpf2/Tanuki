using System;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System;

namespace Game.Gameplay.Board
{
    public interface IReadonlyBoard
    {
        event Action OnHighestNonEmptyRowUpdated;

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        int Rows { get; }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        int Columns { get; }

        int HighestNonEmptyRow { get; }

        IPiece Get(Coordinate coordinate);
    }
}