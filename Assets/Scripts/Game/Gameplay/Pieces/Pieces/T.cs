using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class T : Piece
    {
        public override PieceType? DecomposeType => PieceType.BlockT;

        public T([NotNull] IConverter converter, int id) : base(converter, id, PieceType.T) { }

        public override IPiece Clone()
        {
            return new T(Converter, Id).WithState(State);
        }

        protected override bool[,] GetGrid()
        {
            return new[,] { { false, true, false }, { true, true, true } };
        }
    }
}