using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class PlayerL : Piece
    {
        /*
         *
         * X
         * X
         * XX
         *
         * Has no special behaviour, except for block decomposition on destroy
         *
         */

        public override PieceType? DecomposeType => PieceType.BlockL;

        public PlayerL([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerL) { }

        public override IPiece Clone()
        {
            return new PlayerL(Converter, Id).WithState(State);
        }

        protected override bool[,] GetGrid()
        {
            return new[,] { { true, true }, { true, false }, { true, false } };
        }
    }
}