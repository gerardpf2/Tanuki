using Game.Gameplay.Board.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public class PlayerS : Piece
    {
        /*
         *
         *  XX
         * XX
         *
         * Has no special behaviour
         *
         */

        public PlayerS([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerS) { }

        public override IPiece Clone()
        {
            return new PlayerS(Converter, Id).WithState(State);
        }

        protected override bool[,] GetGrid()
        {
            return new[,] { { true, true, false }, { false, true, true } };
        }
    }
}