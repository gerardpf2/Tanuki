using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class BlockO : RectangularPiece
    {
        /*
         *
         * 1 Row x 1 Column
         *
         * Has no special behaviour
         *
         */

        public override bool CanRotate => false;

        public BlockO([NotNull] IConverter converter, int id) : base(converter, id, PieceType.BlockO, 1, 1) { }

        public override IPiece Clone()
        {
            return new BlockO(Converter, Id).WithState(State);
        }
    }
}