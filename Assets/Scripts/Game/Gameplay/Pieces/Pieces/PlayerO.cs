using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class PlayerO : RectangularPiece
    {
        /*
         *
         * 2 Row x 2 Column
         *
         * Has no special behaviour, except for block decomposition on destroy
         *
         */

        public override bool CanRotate => false;

        public override bool DecomposeOnDestroy => true;

        public PlayerO([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerO, 2, 2) { }

        public override IPiece Clone()
        {
            return new PlayerO(Converter, Id).WithState(State);
        }
    }
}