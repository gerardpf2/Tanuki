using Game.Common.Pieces;
using Game.Gameplay.Pieces.Pieces.Utils;
using Infrastructure.System;
using JetBrains.Annotations;

namespace Game.Gameplay.Pieces.Pieces
{
    public class PlayerI : RectangularPiece
    {
        public override PieceType? DecomposeType => PieceType.BlockI;

        public PlayerI([NotNull] IConverter converter, int id) : base(converter, id, PieceType.PlayerI, 4, 1) { }

        public override IPiece Clone()
        {
            return new PlayerI(Converter, Id).WithState(State);
        }
    }
}