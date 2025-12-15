using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class BlockL : RectangularPiece
    {
        public override bool CanRotate => false;

        public BlockL([NotNull] IConverter converter, int id) : base(converter, id, PieceType.BlockL, 1, 1) { }

        public override IPiece Clone()
        {
            return new BlockL(Converter, Id).WithState(State);
        }
    }
}