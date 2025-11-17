using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class BlockI : RectangularPiece
    {
        public override bool CanRotate => false;

        public BlockI([NotNull] IConverter converter, int id) : base(converter, id, PieceType.BlockI, 1, 1) { }

        public override IPiece Clone()
        {
            return new BlockI(Converter, Id).WithState(State);
        }
    }
}