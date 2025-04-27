using System.Collections.Generic;
using Game.Gameplay.Utils;
using Infrastructure.System;
using Infrastructure.System.Exceptions;

namespace Game.Gameplay.Model.Board.Pieces
{
    public class Test : ITest, ITestUpdater
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

        public bool Alive { get; private set; } = true;

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
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0), Is(ComparisonOperator.LessThan, ITest.Rows)] int eyeRowOffset)
        {
            ArgumentOutOfRangeException.ThrowIfNot(eyeRowOffset, ComparisonOperator.GreaterThanOrEqualTo, 0);
            ArgumentOutOfRangeException.ThrowIfNot(eyeRowOffset, ComparisonOperator.LessThan, ITest.Rows);

            EyeMovementDirectionUp = eyeMovementDirectionUp;
            EyeRowOffset = eyeRowOffset;
        }

        public IEnumerable<Coordinate> GetCoordinates(Coordinate sourceCoordinate)
        {
            for (int rowOffset = 0; rowOffset < ITest.Rows; ++rowOffset)
            {
                yield return sourceCoordinate.WithOffset(rowOffset, 0);
            }
        }

        public void Damage(
            [Is(ComparisonOperator.GreaterThanOrEqualTo, 0), Is(ComparisonOperator.LessThan, ITest.Rows)] int rowOffset,
            [Is(ComparisonOperator.EqualTo, 0)] int columnOffset)
        {
            ArgumentOutOfRangeException.ThrowIfNot(rowOffset, ComparisonOperator.GreaterThanOrEqualTo, 0);
            ArgumentOutOfRangeException.ThrowIfNot(rowOffset, ComparisonOperator.LessThan, ITest.Rows);
            ArgumentOutOfRangeException.ThrowIfNot(columnOffset, ComparisonOperator.EqualTo, 0);

            if (EyeRowOffset != rowOffset)
            {
                return;
            }

            Alive = false;
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
    }
}