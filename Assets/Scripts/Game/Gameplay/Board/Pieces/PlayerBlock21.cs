using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public class PlayerBlock21 : RectangularPiece
    {
        /*
         *
         * 2 Row x 1 Column
         *
         * Has no special behaviour
         *
         */

        public PlayerBlock21([NotNull] IConverter converter, uint id) : base(converter, id, PieceType.PlayerBlock21, 2, 1) { }
    }
}