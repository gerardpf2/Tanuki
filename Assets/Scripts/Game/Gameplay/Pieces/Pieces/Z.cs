using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class Z : Piece
    {
        public override PieceType? DecomposeType => PieceType.BlockZ;

        public Z([NotNull] IConverter converter, int id) : base(converter, id, PieceType.Z) { }

        public override IPiece Clone()
        {
            return new Z(Converter, Id).WithState(State);
        }

        protected override bool[,] GetGrid()
        {
            return new[,] { { false, true, true }, { true, true, false } };
        }
    }
}