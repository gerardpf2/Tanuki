using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class PlayerJ : Piece
    {
        /*
         *
         *  X
         *  X
         * XX
         *
         * Has no special behaviour, except for block decomposition on destroy
         *
         */

        public override PieceType? DecomposeType => PieceType.BlockJ;

        public PlayerJ([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerJ) { }

        public override IPiece Clone()
        {
            return new PlayerJ(Converter, Id).WithState(State);
        }

        protected override bool[,] GetGrid()
        {
            return new[,] { { true, true }, { false, true }, { false, true } };
        }
    }
}