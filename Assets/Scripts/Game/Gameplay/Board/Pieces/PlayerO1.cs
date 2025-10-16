using Game.Gameplay.Board.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public class PlayerO1 : RectangularPiece
    {
        /*
         *
         * 1 Row x 1 Column
         *
         * Has no special behaviour
         *
         */

        public PlayerO1([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerBlock11, 1, 1) { }

        public override IPiece Clone()
        {
            return new PlayerO1(Converter, Id).WithState(State);
        }
    }
}