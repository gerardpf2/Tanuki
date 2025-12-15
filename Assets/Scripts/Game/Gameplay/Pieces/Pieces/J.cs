using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class J : Piece
    {
        public override PieceType? DecomposeType => PieceType.BlockJ;

        public J([NotNull] IConverter converter, int id) : base(converter, id, PieceType.J) { }

        public override IPiece Clone()
        {
            return new J(Converter, Id).WithState(State);
        }

        protected override bool[,] GetGrid()
        {
            return new[,] { { true, true }, { false, true }, { false, true } };
        }
    }
}