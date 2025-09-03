using Infrastructure.System;
using Infrastructure.System.Exceptions;

namespace Game.Gameplay.Board.Pieces
{
    public class Test : RectangularPiece, ITest, ITestUpdater
    {
        /*
         *
         * 3 Row x 1 Column
         *
         * Has an eye that moves from bottom to top, and vice versa
         * Can only be damaged if the eye is hit
         *
         */

        private int _eyeRowOffset;

        public bool EyeMovementDirectionUp { get; private set; }

        [Is(ComparisonOperator.GreaterThanOrEqualTo, 0), Is(ComparisonOperator.LessThan, ITest.Rows)]
        public int EyeRowOffset
        {
            get
            {
                InvalidOperationException.ThrowIfNot(_eyeRowOffset, ComparisonOperator.GreaterThanOrEqualTo, 0);
                InvalidOperationException.ThrowIfNot(_eyeRowOffset, ComparisonOperator.LessThan, ITest.Rows);

                return _eyeRowOffset;
            }
            private set
            {
                ArgumentOutOfRangeException.ThrowIfNot(value, ComparisonOperator.GreaterThanOrEqualTo, 0);
                ArgumentOutOfRangeException.ThrowIfNot(value, ComparisonOperator.LessThan, ITest.Rows);

                _eyeRowOffset = value;
            }
        }

        public Test(
            bool eyeMovementDirectionUp,
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0), Is(ComparisonOperator.LessThan, ITest.Rows)] int eyeRowOffset) : base(PieceType.Test, ITest.Rows, 1)
        {
            ArgumentOutOfRangeException.ThrowIfNot(eyeRowOffset, ComparisonOperator.GreaterThanOrEqualTo, 0);
            ArgumentOutOfRangeException.ThrowIfNot(eyeRowOffset, ComparisonOperator.LessThan, ITest.Rows);

            EyeMovementDirectionUp = eyeMovementDirectionUp;
            EyeRowOffset = eyeRowOffset;
        }

        public void MoveEye()
        {
            EyeMovementDirectionUp = EyeRowOffset switch
            {
                ITest.Rows - 1 when EyeMovementDirectionUp => false,
                0 when !EyeMovementDirectionUp => true,
                _ => EyeMovementDirectionUp
            };

            if (EyeMovementDirectionUp)
            {
                ++EyeRowOffset;
            }
            else
            {
                --EyeRowOffset;
            }
        }

        protected override void HandleDamaged(int rowOffset, int columnOffset)
        {
            if (EyeRowOffset != rowOffset)
            {
                return;
            }

            base.HandleDamaged(rowOffset, columnOffset);
        }
    }
}