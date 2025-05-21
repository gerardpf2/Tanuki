using System;
using System.Collections.Generic;
using Game.Gameplay.Board.Pieces;
using Infrastructure.System;
using JetBrains.Annotations;

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

        [NotNull]
        IEnumerable<KeyValuePair<IPiece, Coordinate>> PieceSourceCoordinates { get; }

        IPiece Get(Coordinate coordinate);
    }
}