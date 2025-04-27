using System.Collections.Generic;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Model.Board
{
    public interface IBoardDefinition
    {
        string Id { get; }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        int Rows { get; }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        int Columns { get; }

        [NotNull, ItemNotNull]
        IEnumerable<IPiecePlacement> PiecePlacements { get; }
    }
}