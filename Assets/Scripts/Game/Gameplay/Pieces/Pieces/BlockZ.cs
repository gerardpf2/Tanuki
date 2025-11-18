using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class BlockZ : RectangularPiece
    {
        /*
         *
         * 1 Row x 1 Column
         *
         * Has no special behaviour
         *
         */

        public override bool CanRotate => false;

        public BlockZ([NotNull] IConverter converter, int id) : base(converter, id, PieceType.BlockZ, 1, 1) { }

        public override IPiece Clone()
        {
            return new BlockZ(Converter, Id).WithState(State);
        }
    }
}