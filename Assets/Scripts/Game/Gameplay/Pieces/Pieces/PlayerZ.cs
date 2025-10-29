using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class PlayerZ : Piece
    {
        /*
         *
         * XX
         *  XX
         *
         * Has no special behaviour
         *
         */

        public PlayerZ([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerZ) { }

        public override IPiece Clone()
        {
            return new PlayerZ(Converter, Id).WithState(State);
        }

        protected override bool[,] GetGrid()
        {
            return new[,] { { false, true, true }, { true, true, false } };
        }
    }
}