using Game.Gameplay.Board.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public class PlayerI : RectangularPiece
    {
        /*
         *
         * 2 Row x 1 Column
         *
         * Has no special behaviour
         *
         */

        public PlayerI([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerI, 2, 1) { }

        public override IPiece Clone()
        {
            return new PlayerI(Converter, Id).WithState(State);
        }
    }
}