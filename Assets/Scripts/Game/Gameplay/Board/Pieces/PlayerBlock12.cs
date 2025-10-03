using Game.Gameplay.Board.Pieces.Utils;
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

        public PlayerBlock12([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerBlock12, 1, 2) { }

        public override IPiece Clone()
        {
            return new PlayerBlock12(Converter, Id).WithState(State);
        }
    }
}