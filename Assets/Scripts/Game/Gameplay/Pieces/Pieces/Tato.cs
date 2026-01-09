using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class Tato : RectangularPiece
    {
        public override bool CanRotate => false;

        public Tato([NotNull] IConverter converter, int id) : base(converter, id, PieceType.Tato, 1, 1) { }

        public override IPiece Clone()
        {
            return new Tato(Converter, Id).WithState(State);
        }
    }
}