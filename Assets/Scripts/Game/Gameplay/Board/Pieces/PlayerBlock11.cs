using Game.Gameplay.Board.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Board.Pieces
{
    public class PlayerBlock11 : RectangularPiece
    {
        /*
         *
         * 1 Row x 1 Column
         *
         * Has no special behaviour
         *
         */

        public PlayerBlock11([NotNull] IConverter converter, uint id) : base(converter, id, PieceType.PlayerBlock11, 1, 1) { }

        public override IPiece Clone()
        {
            return new PlayerBlock11(Converter, Id).WithCustomData(CustomData);
        }
    }
}