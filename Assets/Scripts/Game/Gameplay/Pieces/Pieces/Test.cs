using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
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

        private const int Rows = 3;
        [NotNull] private const string EyeMovementDirectionUpKey = "DIRECTION_UP";
        [NotNull] private const string EyeRowOffsetKey = "ROW_OFFSET";

        public override bool CanRotate => false;

        public bool EyeMovementDirectionUp { get; private set; }

        public int EyeRowOffset { get; private set; }

        public Test([NotNull] IConverter converter, int id) : base(converter, id, PieceType.Test, Rows, 1) { }

        public override IPiece Clone()
        {
            return new Test(Converter, Id).WithState(State);
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

        protected override void AddStateEntries()
        {
            base.AddStateEntries();

            AddStateEntry(EyeMovementDirectionUpKey, EyeMovementDirectionUp);
            AddStateEntry(EyeRowOffsetKey, EyeRowOffset);
        }

        protected override bool ProcessStateEntry(string key, string value)
        {
            switch (key)
            {
                case EyeMovementDirectionUpKey:
                {
                    EyeMovementDirectionUp = Converter.Convert<bool>(value);

                    return true;
                }
                case EyeRowOffsetKey:
                {
                    EyeRowOffset = Converter.Convert<int>(value);

                    return true;
                }
                default:
                    return base.ProcessStateEntry(key, value);
            }
        }

        protected override void HandleDamaged(int nonRotatedRowOffset, int nonRotatedColumnOffset)
        {
            if (EyeRowOffset != nonRotatedRowOffset)
            {
                return;
            }

            base.HandleDamaged(nonRotatedRowOffset, nonRotatedColumnOffset);
        }
    }
}