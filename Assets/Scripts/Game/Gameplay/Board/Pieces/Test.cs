using Infrastructure.System;
using Infrastructure.System.Exceptions;
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

        [NotNull] private readonly IConverter _converter;

        public bool EyeMovementDirectionUp { get; private set; }

        public int EyeRowOffset { get; private set; }

        public Test([NotNull] IConverter converter) : base(converter, PieceType.Test, Rows, 1)
        {
            ArgumentNullException.ThrowIfNull(converter);

            _converter = converter;
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
                    EyeMovementDirectionUp = _converter.Convert<bool>(value);

                    return true;
                }
                case CustomDataEyeRowOffsetKey:
                {
                    EyeRowOffset = _converter.Convert<int>(value);

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