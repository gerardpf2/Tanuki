using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public class PlayerBlock12 : RectangularPiece
    {
        /*
         *
         * 1 Row x 2 Column
         *
         * Has no special behaviour
         *
         */

        public PlayerBlock12([NotNull] IConverter converter) : base(converter, PieceType.PlayerBlock12, 1, 2) { }
    }
}