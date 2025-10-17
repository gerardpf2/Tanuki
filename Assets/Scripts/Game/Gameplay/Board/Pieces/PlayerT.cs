using Game.Gameplay.Board.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public class PlayerT : Piece
    {
        /*
         *
         * XXX
         *  X
         *
         * Has no special behaviour
         *
         */

        public PlayerT([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerT) { }

        public override IPiece Clone()
        {
            return new PlayerT(Converter, Id).WithState(State);
        }

        protected override bool[,] GetGrid()
        {
            return new[,] { { false, true, false }, { true, true, true } };
        }
    }
}