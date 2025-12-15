using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class L : Piece
    {
        public override PieceType? DecomposeType => PieceType.BlockL;

        public L([NotNull] IConverter converter, int id) : base(converter, id, PieceType.L) { }

        public override IPiece Clone()
        {
            return new L(Converter, Id).WithState(State);
        }

        protected override bool[,] GetGrid()
        {
            return new[,] { { true, true }, { true, false }, { true, false } };
        }
    }
}