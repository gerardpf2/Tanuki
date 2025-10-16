using Game.Gameplay.Board.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public class PlayerO : RectangularPiece
    {
        /*
         *
         * 2 Row x 2 Column
         *
         * Has no special behaviour
         *
         */

        public PlayerO([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerO, 2, 2) { }

        public override IPiece Clone()
        {
            return new PlayerO(Converter, Id).WithState(State);
        }
    }
}