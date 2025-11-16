using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class PlayerT : Piece
    {
        /*
         *
         * XXX
         *  X
         *
         * Has no special behaviour, except for block decomposition on destroy
         *
         */

        public override bool DecomposeOnDestroy => true;

        public PlayerT([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerT) { }

        public override IPiece Clone()
        {
            return new PlayerT(Converter, Id).WithState(State);
        }

        protected override bool[,] GetGrid()
        {
            return new[,] { { false, true, false }, { true, true, true } };
        }
    }
}