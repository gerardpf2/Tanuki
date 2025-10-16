using Game.Gameplay.Board.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public class PlayerJ : Piece
    {
        /*
         *
         *  X
         *  X
         * XX
         *
         * Has no special behaviour
         *
         */

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