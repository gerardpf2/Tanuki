using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class Tata : RectangularPiece
    {
        public override bool CanRotate => false;

        public Tata([NotNull] IConverter converter, int id) : base(converter, id, PieceType.Tata, 1, 1) { }

        public override IPiece Clone()
        {
            return new Tata(Converter, Id).WithState(State);
        }
    }
}