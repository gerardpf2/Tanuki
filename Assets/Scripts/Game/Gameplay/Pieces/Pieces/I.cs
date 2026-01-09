using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class I : RectangularPiece
    {
        public override PieceType? DecomposeType => PieceType.BlockI;

        public I([NotNull] IConverter converter, int id) : base(converter, id, PieceType.I, 4, 1) { }

        public override IPiece Clone()
        {
            return new I(Converter, Id).WithState(State);
        }
    }
}