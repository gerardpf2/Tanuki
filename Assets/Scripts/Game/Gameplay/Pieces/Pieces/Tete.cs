using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class Tete : RectangularPiece
    {
        public override bool CanRotate => false;

        public Tete([NotNull] IConverter converter, int id) : base(converter, id, PieceType.Tete, 1, 1) { }

        public override IPiece Clone()
        {
            return new Tete(Converter, Id).WithState(State);
        }
    }
}