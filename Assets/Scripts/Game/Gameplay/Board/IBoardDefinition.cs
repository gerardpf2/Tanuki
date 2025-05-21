using System.Collections.Generic;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board
{
    public interface IBoardDefinition
    {
        string Id { get; }

        string SerializedData { get; }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        int Rows { get; }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        int Columns { get; }

        [NotNull, ItemNotNull]
        IEnumerable<IPiecePlacement> PiecePlacements { get; }
    }
}