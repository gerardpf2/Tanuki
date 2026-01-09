using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class StaticBlock : RectangularPiece
    {
        public override bool CanRotate => false;

        public override bool AffectedByGravity => false;

        public StaticBlock([NotNull] IConverter converter, int id) : base(converter, id, PieceType.StaticBlock, 1, 1) { }

        public override IPiece Clone()
        {
            return new StaticBlock(Converter, Id).WithState(State);
        }
    }
}