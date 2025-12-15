using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class BlockT : RectangularPiece
    {
        public override bool CanRotate => false;

        public BlockT([NotNull] IConverter converter, int id) : base(converter, id, PieceType.BlockT, 1, 1) { }

        public override IPiece Clone()
        {
            return new BlockT(Converter, Id).WithState(State);
        }
    }
}