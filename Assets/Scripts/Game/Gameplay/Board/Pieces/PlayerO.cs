using Game.Gameplay.Board.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public class PlayerO : RectangularPiece
    {
        /*
         *
         * 1 Row x 1 Column
         *
         * Has no special behaviour
         *
         */

        public PlayerO([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerO, 1, 1) { }

        public override IPiece Clone()
        {
            return new PlayerO(Converter, Id).WithState(State);
        }
    }
}