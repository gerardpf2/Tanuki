using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class PlayerS : Piece
    {
        public override PieceType? DecomposeType => PieceType.BlockS;

        public PlayerS([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerS) { }

        public override IPiece Clone()
        {
            return new PlayerS(Converter, Id).WithState(State);
        }

        protected override bool[,] GetGrid()
        {
            return new[,] { { true, true, false }, { false, true, true } };
        }
    }
}