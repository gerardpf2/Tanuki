using Infrastructure.System;

namespace Game.Gameplay.Model.Board
{
    public interface IPiecePlacement
    {
        PieceType PieceType { get; }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        int Row { get; }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0)]
        int Column { get; }
    }
}