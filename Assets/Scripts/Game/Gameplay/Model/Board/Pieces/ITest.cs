using Infrastructure.System;

namespace Game.Gameplay.Model.Board.Pieces
{
    public interface ITest : IPiece
    {
        public const int Rows = 3;

        bool EyeMovementDirectionUp { get; }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0), Is(ComparisonOperator.LessThan, Rows)]
        int EyeRowOffset { get; }
    }
}