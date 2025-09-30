using Game.Gameplay.Board.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public class Test : RectangularPiece, ITest
    {
        /*
         *
         * 3 Row x 1 Column
         *
         * Has an eye that moves from bottom to top, and vice versa
         * Can only be damaged if the eye is hit
         *
         */

        public const int Rows = 3;
        private const string CustomDataEyeMovementDirectionUpKey = "EyeMovementDirectionUp"; // TODO: Use shorter key
        private const string CustomDataEyeRowOffsetKey = "EyeRowOffset"; // TODO: Use shorter key

        public bool EyeMovementDirectionUp { get; private set; }

        public int EyeRowOffset { get; private set; }

        public Test([NotNull] IConverter converter, uint id) : base(converter, id, PieceType.Test, Rows, 1) { }

        public override IPiece Clone()
        {
            return new Test(Converter, Id).WithCustomData(CustomData);
        }

        public void MoveEye()
        {
            EyeMovementDirectionUp = EyeRowOffset switch
            {
                Rows - 1 when EyeMovementDirectionUp => false,
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

        protected override void AddCustomDataEntries()
        {
            base.AddCustomDataEntries();

            AddCustomDataEntry(CustomDataEyeMovementDirectionUpKey, EyeMovementDirectionUp);
            AddCustomDataEntry(CustomDataEyeRowOffsetKey, EyeRowOffset);
        }

        protected override bool ProcessCustomDataEntry(string key, string value)
        {
            switch (key)
            {
                case CustomDataEyeMovementDirectionUpKey:
                {
                    EyeMovementDirectionUp = Converter.Convert<bool>(value);

                    return true;
                }
                case CustomDataEyeRowOffsetKey:
                {
                    EyeRowOffset = Converter.Convert<int>(value);

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