using System.Collections.Generic;
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

        private const string CustomDataEyeMovementDirectionUpKey = "EyeMovementDirectionUp";
        private const string CustomDataEyeRowOffsetKey = "EyeRowOffset";

        private int _eyeRowOffset;

        public override IEnumerable<KeyValuePair<string, object>> CustomData
        {
            get
            {
                IDictionary<string, object> customData = new Dictionary<string, object>
                {
                    { CustomDataEyeMovementDirectionUpKey, EyeMovementDirectionUp },
                    { CustomDataEyeRowOffsetKey, EyeRowOffset },
                };

                IEnumerable<KeyValuePair<string, object>> baseCustomData = base.CustomData;

                if (baseCustomData is not null)
                {
                    foreach ((string key, object value) in baseCustomData)
                    {
                        customData.Add(key, value);
                    }
                }

                return customData;
            }
        }

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

        public Test(IEnumerable<KeyValuePair<string, object>> customData) : base(PieceType.Test, customData, ITest.Rows, 1) { }

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

        protected override bool ProcessCustomDataEntry(string key, object value)
        {
            switch (key)
            {
                case CustomDataEyeMovementDirectionUpKey:
                {
                    EyeMovementDirectionUp = (bool)value;

                    return true;
                }
                case CustomDataEyeRowOffsetKey:
                {
                    int eyeRowOffset = (int)(long)value; // TODO

                    ArgumentOutOfRangeException.ThrowIfNot(eyeRowOffset, ComparisonOperator.GreaterThanOrEqualTo, 0);
                    ArgumentOutOfRangeException.ThrowIfNot(eyeRowOffset, ComparisonOperator.LessThan, ITest.Rows);

                    EyeRowOffset = eyeRowOffset;

                    return true;
                }
                default:
                    return base.ProcessCustomDataEntry(key, value);
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