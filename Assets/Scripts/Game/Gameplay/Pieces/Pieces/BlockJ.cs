using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class BlockJ : RectangularPiece
    {
        /*
         *
         * 1 Row x 1 Column
         *
         * Has no special behaviour
         *
         */

        public BlockJ([NotNull] IConverter converter, int id) : base(converter, id, PieceType.BlockJ, 1, 1) { }

        public override IPiece Clone()
        {
            return new BlockJ(Converter, Id).WithState(State);
        }
    }
}