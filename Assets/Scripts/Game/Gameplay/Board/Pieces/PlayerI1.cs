using Game.Gameplay.Board.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public class PlayerI1 : RectangularPiece
    {
        /*
         *
         * 2 Row x 1 Column
         *
         * Has no special behaviour
         *
         */

        public PlayerI1([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerBlock21, 2, 1) { }

        public override IPiece Clone()
        {
            return new PlayerI1(Converter, Id).WithState(State);
        }
    }
}