using Game.Gameplay.Board.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public class PlayerL : Piece
    {
        /*
         *
         * X
         * X
         * XX
         *
         * Has no special behaviour
         *
         */

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