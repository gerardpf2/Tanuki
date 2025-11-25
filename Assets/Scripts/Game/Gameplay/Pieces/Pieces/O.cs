using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class O : RectangularPiece
    {
        public override bool CanRotate => false;

        public override PieceType? DecomposeType => PieceType.BlockO;

        public O([NotNull] IConverter converter, int id) : base(converter, id, PieceType.O, 2, 2) { }

        public override IPiece Clone()
        {
            return new O(Converter, Id).WithState(State);
        }
    }
}